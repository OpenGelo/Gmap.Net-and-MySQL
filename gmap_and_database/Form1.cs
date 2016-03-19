using MetroFramework.Forms;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using MySQLClass;
using MySql.Data;
using MySql.Data.MySqlClient;
using GMap.NET.Projections;
using System.Globalization;
using System.Reflection;
using System.Net.NetworkInformation;

namespace gmap_and_database
{
    public partial class Form1 : MetroForm
    {
        //static PointLatLng copterPos = new PointLatLng(47.402489, 19.071558);       //Just the corrds of my flying place
        static PointLatLng copterPos = new PointLatLng(-6.9349205, 107.6519021);

        List<PointLatLng> points = new List<PointLatLng>();
        // polygons
        GMapPolygon polygon;
        PointLatLng GPS_pos, GPS_pos_old;
        PointLatLng end;
        PointLatLng start;
        // marker Gmap
        GMapMarker center;
        GMarkerGoogle currentMarker;
        GMapMarkerRect CurentRectMarker = null;
        //float latitude, longitude;

        // layers
        readonly GMapOverlay top = new GMapOverlay();
        internal readonly GMapOverlay objects = new GMapOverlay("objects");
        internal readonly GMapOverlay routes = new GMapOverlay("routes");
        internal readonly GMapOverlay polygons = new GMapOverlay("polygons");
        
        static bool isMouseDown = false;
        static bool isMouseDraging = false;

        public Form1()
        {
            InitializeComponent();

            //added by riyadhi

            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            identify_user();
           
            //end added

            center = new GMarkerGoogle(MainMap.Position, GMarkerGoogleType.blue_dot);

            // set cache mode only if no internet avaible
            if (!Stuff.PingNetwork("pingtest.com"))
            {
                MainMap.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection available, going to CacheOnly mode.", "GMap.NET - Demo.WindowsForms", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // get zoom  
            trackBar1.Minimum = MainMap.MinZoom * 100;
            trackBar1.Maximum = MainMap.MaxZoom * 100;
            trackBar1.TickFrequency = 100;

            

            

            //config map
            MainMap.CacheLocation = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + "gmapcache" + Path.DirectorySeparatorChar;
            MainMap.MapProvider = GoogleMapProvider.Instance;
            MainMap.Position = new PointLatLng(-6.976957, 107.630241);
            MainMap.MinZoom = 0;
            MainMap.MaxZoom = 24;
            MainMap.Zoom = 18;
            // set current marker
            currentMarker = new GMarkerGoogle(MainMap.Position, GMarkerGoogleType.green_dot);
            currentMarker.IsHitTestVisible = true;
            top.Markers.Add(currentMarker);
            MainMap.Overlays.Add(top);
            // map events
            MainMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
            MainMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
            MainMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);
            MainMap.MouseDoubleClick += new MouseEventHandler(MainMap_MouseDoubleClick);
            MainMap.OnPositionChanged += new PositionChanged(MainMap_OnPositionChanged);

            MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);

            MainMap.OnMapZoomChanged += new MapZoomChanged(MainMap_OnMapZoomChanged);
            MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);

            MainMap.OnMarkerClick += new MarkerClick(MainMap_OnMarkerClick);
            MainMap.OnMarkerEnter += new MarkerEnter(MainMap_OnMarkerEnter);
            MainMap.OnMarkerLeave += new MarkerLeave(MainMap_OnMarkerLeave);

            MainMap.OnPolygonEnter += new PolygonEnter(MainMap_OnPolygonEnter);
            MainMap.OnPolygonLeave += new PolygonLeave(MainMap_OnPolygonLeave);

            MainMap.OnRouteEnter += new RouteEnter(MainMap_OnRouteEnter);
            MainMap.OnRouteLeave += new RouteLeave(MainMap_OnRouteLeave);

            MainMap.Manager.OnTileCacheComplete += new TileCacheComplete(OnTileCacheComplete);
            MainMap.Manager.OnTileCacheStart += new TileCacheStart(OnTileCacheStart);
            MainMap.Manager.OnTileCacheProgress += new TileCacheProgress(OnTileCacheProgress);
            //MainMap.OnPositionChanged += new PositionChanged(MainMap_OnCurrentPositionChanged);
            //MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            //MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
            //MainMap.OnMarkerClick += new MarkerClick(MainMap_OnMarkerClick);
            /*MainMap.OnMapZoomChanged += new MapZoomChanged(MainMap_OnMapZoomChanged);
            MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
            MainMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
            MainMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
            MainMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);
            MainMap.OnMarkerEnter += new MarkerEnter(MainMap_OnMarkerEnter);
            MainMap.OnMarkerLeave += new MarkerLeave(MainMap_OnMarkerLeave); */

            //comboBoxMapType.DataSource = Enum.GetValues(typeof(AccessMode));
            //comboBoxMapType.SelectedItem = MainMap.Manager.Mode;
            MainMap.MapScaleInfoEnabled = false;
            MainMap.ScalePen = new Pen(Color.Red);

            //GPS_pos.Lat = 47.402489;
            //GPS_pos.Lng = 19.071558;

        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("adasas");
            //Application.Exit();
            Environment.Exit(0);
        }

        void identify_user()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=root";
            string Query = "SELECT *FROM mapdatabase.user_table WHERE user_name = '"+GlobalVar.userActive+"';";
            MySqlConnection ConnDb = new MySqlConnection(constring);
            MySqlCommand sqlCommand = new MySqlCommand(Query, ConnDb);
            MySqlDataReader myReader;

            string sRole;

            try
            {
                ConnDb.Open();
                myReader = sqlCommand.ExecuteReader();

                while (myReader.Read())
                {
                    string sId = myReader.GetInt32("iduser").ToString();
                    //string sName = myReader.GetString
                    sRole = myReader.GetString("role");

                    if (sRole == "User")
                    {
                        manageUserToolStripMenuItem.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        #region -- map events --

        void OnTileCacheComplete()
        {
            //Debug.WriteLine("OnTileCacheComplete");
            long size = 0;
            int db = 0;
            try
            {
                DirectoryInfo di = new DirectoryInfo(MainMap.CacheLocation);
                var dbs = di.GetFiles("*.gmdb", SearchOption.AllDirectories);
                foreach (var d in dbs)
                {
                    size += d.Length;
                    db++;
                }
            }
            catch
            {
            }

            if (!IsDisposed)
            {
                MethodInvoker m = delegate
                {
                    //textBoxCacheSize.Text = string.Format(CultureInfo.InvariantCulture, "{0} db in {1:00} MB", db, size / (1024.0 * 1024.0));
                    //textBoxCacheStatus.Text = "all tiles saved!";
                };

                if (!IsDisposed)
                {
                    try
                    {
                        Invoke(m);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        void OnTileCacheStart()
        {
            //Debug.WriteLine("OnTileCacheStart");

            if (!IsDisposed)
            {
                MethodInvoker m = delegate
                {
                    //textBoxCacheStatus.Text = "saving tiles...";
                };
                Invoke(m);
            }
        }

        void OnTileCacheProgress(int left)
        {
            if (!IsDisposed)
            {
                MethodInvoker m = delegate
                {
                    //textBoxCacheStatus.Text = left + " tile to save...";
                };
                Invoke(m);
            }
        }

        void MainMap_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMapMarkerRect)
            {
                CurentRectMarker = null;

                GMapMarkerRect rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Blue;

                //Debug.WriteLine("OnMarkerLeave: " + item.Position);
            }
        }

        void MainMap_OnMarkerEnter(GMapMarker item)
        {
            metroTextBox10.Text = Convert.ToString(item.Position.Lat);
            metroTextBox11.Text = Convert.ToString(item.Position.Lng);
            /*string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            //MySqlCommand cmdDataBase = new MySqlCommand("select id,name,age from database.edata ;", conDataBase);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from mapdatabase.maptable where Latitude='" + metroTextBox10.Text + "' AND Longitude='"+metroTextBox11.Text , conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                metroLabel7.Text = dataGridView1.RowCount.ToString();
                int count = 0;
                while (myReader.Read())
                    count = count + 1;

                if (count == 1)
                {
                    GMarkerGoogle m = new GMarkerGoogle(currentMarker.Position, GMarkerGoogleType.yellow);
                    GMapMarkerRect mBorders = new GMapMarkerRect(currentMarker.Position);
                    {
                        mBorders.InnerMarker = m;
                        mBorders.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    }
                    mBorders.ToolTipText = myReader[1].ToString();   
                }
                else if (count > 1)
                    MessageBox.Show("Access denied, duplicate username");
                else
                    MessageBox.Show("Data isn't Found");
            }
            catch
            {
 
            }*/
        
            if (item is GMapMarkerRect)
            {
                GMapMarkerRect rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Red;

                CurentRectMarker = rc;
            }
            //Debug.WriteLine("OnMarkerEnter: " + item.Position);
        }

        GMapPolygon currentPolygon = null;
        void MainMap_OnPolygonLeave(GMapPolygon item)
        {
            currentPolygon = null;
            item.Stroke.Color = Color.MidnightBlue;
            //Debug.WriteLine("OnPolygonLeave: " + item.Name);
        }

        void MainMap_OnPolygonEnter(GMapPolygon item)
        {
            currentPolygon = item;
            item.Stroke.Color = Color.Red;
            //Debug.WriteLine("OnPolygonEnter: " + item.Name);
        }

        GMapRoute currentRoute = null;
        void MainMap_OnRouteLeave(GMapRoute item)
        {
            currentRoute = null;
            item.Stroke.Color = Color.MidnightBlue;
            //Debug.WriteLine("OnRouteLeave: " + item.Name);
        }

        void MainMap_OnRouteEnter(GMapRoute item)
        {
            currentRoute = item;
            item.Stroke.Color = Color.Red;
            //Debug.WriteLine("OnRouteEnter: " + item.Name);
        }

        void MainMap_OnMapTypeChanged(GMapProvider type)
        {
            comboBoxMapType.SelectedItem = type;

            trackBar1.Minimum = MainMap.MinZoom * 100;
            trackBar1.Maximum = MainMap.MaxZoom * 100;

            //if (radioButtonFlight.Checked)
            
                MainMap.ZoomAndCenterMarkers("objects");
            
        }

        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            end = MainMap.FromLocalToLatLng(e.X, e.Y);

            if (isMouseDown) // mouse down on some other object and dragged to here.
            {
                if (e.Button == MouseButtons.Left)
                {
                    isMouseDown = false;
                }
                if (!isMouseDraging)
                {
                    if (CurentRectMarker != null)
                    {
                        // cant add WP in existing rect
                    }
                    else
                    {
                        //addWP("WAYPOINT", 0, currentMarker.Position.Lat, currentMarker.Position.Lng, iDefAlt);
                    }
                }
                else
                {
                    if (CurentRectMarker != null)
                    {
                        //update existing point in datagrid
                    }
                }
            }
            //if (comm.isOpen() == true) timer1.Start();
            isMouseDraging = false;
            /*if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }*/
        }

        // add demo circle
        void MainMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            //string constring = "datasource=" + metroTextBox1.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            
            GMarkerGoogle m = new GMarkerGoogle(currentMarker.Position, GMarkerGoogleType.yellow);
            GMapMarkerRect mBorders = new GMapMarkerRect(currentMarker.Position);
            {
                mBorders.InnerMarker = m;
                if (polygon != null)
                {
                    // mBorders.Tag = polygon.Points.Count;
                }
                mBorders.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            }

            Placemark? p = null;
            //if (checkBoxPlacemarkInfo.Checked)
            //{
            
                //mBorders.ToolTipText = currentMarker.Position.ToString();
            mBorders.ToolTipText = "ha";
            
            string Query = "insert into " + metroTextBox2.Text + ".maptable (No,Name,Lattitude,Longitude) values ('2','" + metroTextBox6.Text +"','" + Convert.ToString(currentMarker.Position.Lat) + "','" + Convert.ToString(currentMarker.Position.Lng) + "');";
            //MySqlConnection conDataBase = new MySqlConnection(constring);
            //MySqlCommand cmdDataBase = new MySqlCommand(Query, conData Base);
            objects.Markers.Add(m);
            objects.Markers.Add(mBorders);
            MainMap.Overlays.Add(objects);
            //var cc = new GMapMarkerCircle(MainMap.FromLocalToLatLng(e.X, e.Y));
            //objects.Markers.Add(cc);
        }

        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            start = MainMap.FromLocalToLatLng(e.X, e.Y);

            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Alt)
            {
                isMouseDown = true;
                isMouseDraging = false;

                if (currentMarker.IsVisible)
                {
                    currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                }
            }
            /*if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;

                if (currentMarker.IsVisible)
                {
                    currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);

                    var px = MainMap.MapProvider.Projection.FromLatLngToPixel(currentMarker.Position.Lat, currentMarker.Position.Lng, (int)MainMap.Zoom);
                    var tile = MainMap.MapProvider.Projection.FromPixelToTileXY(px);

                    //Debug.WriteLine("MouseDown: geo: " + currentMarker.Position + " | px: " + px + " | tile: " + tile);
                }
            }*/
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng point = MainMap.FromLocalToLatLng(e.X, e.Y);

            //currentMarker.Position = point;
            metroLabel6.Text = "Lat:" + String.Format("{0:0.000000}", point.Lat) + " Lon:" + String.Format("{0:0.000000}", point.Lng);

            if (!isMouseDown)
            {

            }


            //draging
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                isMouseDraging = true;
                if (CurentRectMarker == null) // left click pan
                {
                    double latdif = start.Lat - point.Lat;
                    double lngdif = start.Lng - point.Lng;
                    MainMap.Position = new PointLatLng(center.Position.Lat + latdif, center.Position.Lng + lngdif);
                }
                else
                {
                    //if (comm.isOpen() == true) timer1.Stop();
                    PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);
                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = pnew;
                    }
                    CurentRectMarker.Position = pnew;

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        CurentRectMarker.InnerMarker.Position = pnew;
                        GPS_pos = pnew;
                    }
                }
            }
            /*if (e.Button == MouseButtons.Left && isMouseDown)
            {
                if (CurentRectMarker == null)
                {
                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
                    }
                }
                else // move rect marker
                {
                    PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

                    int? pIndex = (int?)CurentRectMarker.Tag;
                    if (pIndex.HasValue)
                    {
                        if (pIndex < polygon.Points.Count)
                        {
                            polygon.Points[pIndex.Value] = pnew;
                            MainMap.UpdatePolygonLocalPosition(polygon);
                        }
                    }

                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = pnew;
                    }
                    CurentRectMarker.Position = pnew;

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        CurentRectMarker.InnerMarker.Position = pnew;
                    }
                }

                MainMap.Refresh(); // force instant invalidation
            }*/
        }

        
            // MapZoomChanged
        void MainMap_OnMapZoomChanged()
        {
            if (MainMap.Zoom > 0)
            {
                //tb_mapzoom.Value = (int)(MainMap.Zoom);
                center.Position = MainMap.Position;
            }

            //trackBar1.Value = (int)(MainMap.Zoom * 100.0);
            //textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
        }

        // click on some marker
        void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (item is GMapMarkerRect)
                {
                    GeoCoderStatusCode status;
                    var pos = GMapProviders.GoogleMap.GetPlacemark(item.Position, out status);
                    if (status == GeoCoderStatusCode.G_GEO_SUCCESS && pos != null)
                    {
                        GMapMarkerRect v = item as GMapMarkerRect;
                        {
                            v.ToolTipText = pos.Value.Address;
                        }
                        MainMap.Invalidate(false);
                    }
                }
                else
                {
                    if (item.Tag != null)
                    {
                        /*if (currentTransport != null)
                        {
                            currentTransport.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            currentTransport = null;
                        }
                        currentTransport = item;
                        currentTransport.ToolTipMode = MarkerTooltipMode.Always;*/
                        ;
                    }
                }
            }
        }

        // loader start loading tiles
        void MainMap_OnTileLoadStart()
        {
            MethodInvoker m = delegate()
            {
                //panelMenu.Text = "Menu: loading tiles...";
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // loader end loading tiles
        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            //MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            MethodInvoker m = delegate()
            {
                //panelMenu.Text = "Menu, last load in " + MainMap.ElapsedMilliseconds + "ms";

                //textBoxMemory.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00} MB of {1:0.00} MB", MainMap.Manager.MemoryCache.Size, MainMap.Manager.MemoryCache.Capacity);
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // current point changed
        void MainMap_OnPositionChanged(PointLatLng point)
        {
           
        
            if (point.Lat > 90) { point.Lat = 90; }
            if (point.Lat < -90) { point.Lat = -90; }
            if (point.Lng > 180) { point.Lng = 180; }
            if (point.Lng < -180) { point.Lng = -180; }
            center.Position = point;
            //LMousePos.Text = "Lat:" + String.Format("{0:0.000000}", point.Lat) + " Lon:" + String.Format("{0:0.000000}", point.Lng);

         //on current position changed
            //textBoxLatCurrent.Text = point.Lat.ToString(CultureInfo.InvariantCulture);
            //textBoxLngCurrent.Text = point.Lng.ToString(CultureInfo.InvariantCulture);

            //lock (flights)
            //{
                lastPosition = point;
                lastZoom = (int)MainMap.Zoom;
            //}
        }

        PointLatLng lastPosition;
        int lastZoom;

        // center markers on start
        private void MainForm_Load(object sender, EventArgs e)
        {
            trackBar1.Value = (int)MainMap.Zoom * 100;
            Activate();
            TopMost = true;
            TopMost = false;
        }
        #endregion

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ConnectDB();
        }

        void ConnectDB()
        {
            //string constring = "datasource=localhost;database=database;username=root;password=root";
            //string constring = "datasource=localhost;database=database_map;username=root;password=root";
            string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            //MySqlCommand cmdDataBase = new MySqlCommand("select id,name,age from database.edata ;", conDataBase);
            MySqlCommand cmdDataBase = new MySqlCommand("select No,Name,Lattitude,Longitude from mapdatabase.maptable ;", conDataBase);

            try
            {
                conDataBase.Open();
                MessageBox.Show("Connected to database");
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    


        private void comboBoxMapType_SelectedValueChanged(object sender, EventArgs e)
        {
            //MainMap.MapProvider = comboBoxMapType.SelectedItem as GMapProvider;
            //MainMap.MapProvider = (GMapProvider)comboBoxMapType.SelectedItem;
            //config["MapType"] = comboBoxMapType.Text;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                
                    MainMap.Zoom = trackBar1.Value;
                
            }
            catch { }
        }

       
        public class GMapMarkerRect : GMapMarker
    {
        public Pen Pen = new Pen(Brushes.White, 2);

        public Color Color { get { return Pen.Color; } set { Pen.Color = value; } }

        public GMapMarker InnerMarker;

        public int wprad = 0;
        public GMapControl MainMap;

        public GMapMarkerRect(PointLatLng p)
            : base(p)
        {
            Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            // do not forget set Size of the marker
            // if so, you shall have no event on it ;}
            Size = new System.Drawing.Size(50, 50);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2 - 20);
        }

        public override void OnRender(Graphics g)
        {
            base.OnRender(g);

            if (wprad == 0 || MainMap == null)
                return;

            // undo autochange in mouse over
            if (Pen.Color == Color.Blue)
                Pen.Color = Color.White;

            double width = (MainMap.MapProvider.Projection.GetDistance(MainMap.FromLocalToLatLng(0, 0), MainMap.FromLocalToLatLng(MainMap.Width, 0)) * 1000.0);
            double height = (MainMap.MapProvider.Projection.GetDistance(MainMap.FromLocalToLatLng(0, 0), MainMap.FromLocalToLatLng(MainMap.Height, 0)) * 1000.0);
            double m2pixelwidth = MainMap.Width / width;
            double m2pixelheight = MainMap.Height / height;

            GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * wprad * 2)), LocalPosition.Y);// MainMap.FromLatLngToLocal(wpradposition);
            g.DrawArc(Pen, new System.Drawing.Rectangle((int)(LocalPosition.X - Offset.X - (Math.Abs(loc.X - LocalPosition.X) / 2)), (int)(LocalPosition.Y - Offset.Y - Math.Abs(loc.X - LocalPosition.X) / 2), (int)(Math.Abs(loc.X - LocalPosition.X)), (int)(Math.Abs(loc.X - LocalPosition.X))), 0, 360);

        }
    }

        public class GUI_settings
        {
            public int iMapProviderSelectedIndex { get; set; }
            public GUI_settings()
            {
                iMapProviderSelectedIndex = 1;  //Bing Map
            }
        }

        public class Stuff
        {
            public static bool PingNetwork(string hostNameOrAddress)
            {
                bool pingStatus = false;

                using (Ping p = new Ping())
                {
                    byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    int timeout = 4444; // 4s

                    try
                    {
                        PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                        pingStatus = (reply.Status == IPStatus.Success);
                    }
                    catch (Exception)
                    {
                        pingStatus = false;
                    }
                }

                return pingStatus;
            }
        }

# region wmsprovider
        public class WMSProvider : GMapProvider
    {
        public static readonly WMSProvider Instance;

        WMSProvider()
        {
        }

        static WMSProvider()
        {
            Instance = new WMSProvider();

            Type mytype = typeof(GMapProviders);
            FieldInfo field = mytype.GetField("DbHash", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<int, GMapProvider> list = (Dictionary<int, GMapProvider>)field.GetValue(Instance);

            list.Add(Instance.DbId, Instance);
        }

        #region GMapProvider Members

        readonly Guid id = new Guid("4574218D-B552-4CAF-89AE-F20951BBDB2B");
        public override Guid Id
        {
            get
            {
                return id;
            }
        }

        readonly string name = "WMS Custom";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
                {
                    overlays = new GMapProvider[] { this };
                }
                return overlays;
            }
        }

        public override PureProjection Projection
        {
            get
            {
                return MercatorProjection.Instance;
            }
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);

            return GetTileImageUsingHttp(url);
        }

        #endregion

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            var px1 = Projection.FromTileXYToPixel(pos);
            var px2 = px1;

            px1.Offset(0, Projection.TileSize.Height);
            PointLatLng p1 = Projection.FromPixelToLatLng(px1, zoom);

            px2.Offset(Projection.TileSize.Width, 0);
            PointLatLng p2 = Projection.FromPixelToLatLng(px2, zoom);

            string ret;

            string extra = "?";

            if (CustomWMSURL.Contains("?"))
                extra = "&";

            //if there is a layer, use it  
            if (szWmsLayer != "")
            {
                ret = string.Format(CultureInfo.InvariantCulture, CustomWMSURL + extra+"VERSION=1.1.1&REQUEST=GetMap&SERVICE=WMS&layers=" + szWmsLayer + "&styles=&bbox={0},{1},{2},{3}&width={4}&height={5}&srs=EPSG:4326&format=image/png", p1.Lng, p1.Lat, p2.Lng, p2.Lat, Projection.TileSize.Width, Projection.TileSize.Height);
            }
            else
            {
                ret = string.Format(CultureInfo.InvariantCulture, CustomWMSURL + extra+ "VERSION=1.1.1&REQUEST=GetMap&SERVICE=WMS&styles=&bbox={0},{1},{2},{3}&width={4}&height={5}&srs=EPSG:4326&format=image/png", p1.Lng, p1.Lat, p2.Lng, p2.Lat, Projection.TileSize.Width, Projection.TileSize.Height);
            }

            return ret;
        }

        public static string szWmsLayer = "";
        public static string CustomWMSURL = "http://mapbender.wheregroup.com/cgi-bin/mapserv?map=/data/umn/osm/osm_basic.map";
        } 
#endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //config map
            //MainMap.CacheLocation = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + "gmapcache" + Path.DirectorySeparatorChar;
            //MainMap.MapProvider = GoogleMapProvider.Instance;
            // get map type
            //comboBoxMapType.ValueMember = "Name";
            comboBoxMapType.DataSource = GMapProviders.List.ToArray();
            comboBoxMapType.SelectedItem = MainMap.MapProvider;

            comboBoxMapType.SelectedValueChanged += new System.EventHandler(this.comboBoxMapType_SelectedValueChanged);
            
            MainMap.Zoom = 18;
            MainMap.Invalidate(false);

            int w = MainMap.Size.Width;
            MainMap.Width = w + 1;
            MainMap.Width = w;
            MainMap.ShowCenter = false;
            //trackBar1.Value = (int)MainMap.Zoom * 100;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //string constring = "datasource=localhost;port=3305;username=aptrgdev;password=root";
            string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            //string constring = "datasource=" + metroTextBox1.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            string Query = "insert into " + metroTextBox2.Text + ".maptable (No,Name,Lattitude,Longitude) values (@name,@lat,@lon);";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conDataBase;
            cmd.Connection.Open();
            cmd.CommandText = Query;
            cmd.Parameters.AddWithValue("@name", metroTextBox6.Text);
            cmd.Parameters.AddWithValue("@lat", metroTextBox7.Text);
            cmd.Parameters.AddWithValue("@lon", metroTextBox8.Text);
            cmd.ExecuteNonQuery();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;
            try
            {
                //conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                
                GPS_pos.Lat = Convert.ToDouble(metroTextBox7.Text);
                GPS_pos.Lng = Convert.ToDouble(metroTextBox8.Text);
                //GMarkerGoogle a = new GMarkerGoogle()
                GMarkerGoogle m = new GMarkerGoogle(GPS_pos, GMarkerGoogleType.yellow);
               

                objects.Markers.Add(m);
                
                MainMap.Overlays.Add(objects);
                MessageBox.Show("Added");
                
                //conDataBase.Open();
                //myReader = cmdDataBase.ExecuteReader();
                int count = 0;
                metroLabel7.Text = dataGridView1.RowCount.ToString();
                
                /*while (myReader.Read())
                {
                    count = count + 1;
                    if (count == 1)
                        {
                            MessageBox.Show("Data Found!");
                            metroTextBox7.Text = myReader[2].ToString();
                            metroTextBox8.Text = myReader[3].ToString();
                            
                        }
                }*/

            }
            
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("update " + metroTextBox2.Text + ".maptable set No='1',Name='"+ metroTextBox6.Text+"',Lattitude='"+ metroTextBox7.Text+"',Longitude='"+ metroTextBox8.Text, conDataBase);
            conDataBase.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmdDataBase;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            BindingSource bSource = new BindingSource();

            bSource.DataSource = dbdataset;
            dataGridView1.DataSource = bSource;
            sda.Update(dbdataset);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("DELETE FROM " + metroTextBox2.Text +".maptable WHERE "+metroTextBox6.Text, conDataBase);
            conDataBase.Open();
            /*MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = cmdDataBase;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            BindingSource bSource = new BindingSource();

            bSource.DataSource = dbdataset;
            dataGridView1.DataSource = bSource;
            sda.Update(dbdataset);*/
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            //string constring = "datasource=localhost;database=database;username=root;password=root";
            //string constring = "datasource=localhost;database=database_map;username=root;password=root";
            string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            //MySqlCommand cmdDataBase = new MySqlCommand("select id,name,age from database.edata ;", conDataBase);
            MySqlCommand cmdDataBase = new MySqlCommand("select No,Name,Lattitude,Longitude from mapdatabase.maptable ;", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                //MessageBox.Show("Connected");
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBoxMapType_DropDownClosed(object sender, EventArgs e)
        {
            MainMap.MapProvider = comboBoxMapType.SelectedItem as GMapProvider;
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            string constring = "datasource=" + metroTextBox1.Text + ";database=" + metroTextBox2.Text + ";username=" + metroTextBox3.Text + ";password=" + metroTextBox4.Text + ";port=" + Convert.ToInt32(metroTextBox5.Text);
            MySqlConnection conDataBase = new MySqlConnection(constring);
            //MySqlCommand cmdDataBase = new MySqlCommand("select id,name,age from database.edata ;", conDataBase);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from mapdatabase.maptable where name='" + metroTextBox6.Text + "' ", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                metroLabel7.Text = dataGridView1.RowCount.ToString();
                int count = 0;
                while (myReader.Read())
                    count = count + 1;

                if (count == 1)
                {
                    MessageBox.Show("Data is Found!");
                    metroTextBox7.Text = myReader[2].ToString();
                    metroTextBox8.Text = myReader[3].ToString();
                    
                }
                else if (count > 1)
                    MessageBox.Show("Access denied, duplicate username");
                else
                    MessageBox.Show("Data isn't Found");
            }
            catch
            {
 
            }
        }

        private void manageUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManageUser manage_user = new FormManageUser();
            manage_user.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
            //Application.Exit();
            
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }

        
     }

 }

