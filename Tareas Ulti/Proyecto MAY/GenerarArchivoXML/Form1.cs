using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GenerarArchivoXML
{
    public partial class Form1 : Form
    {

        string ruta = @"C:\XML\Articulos.xml";
        int i;
        string imagenserailizadaedicion;

        public Form1()
        {

            InitializeComponent();

            txtID.Enabled = false; txtDetalles.Enabled = false; txtNombre.Enabled = false; txtPrecio.Enabled = false; btnGuardar.Enabled = false;
            txtID.KeyPress += TxtID_KeyPress;txtNombre.KeyPress += TxtNombre_KeyPress; txtDetalles.KeyPress += TxtDetalles_KeyPress; txtPrecio.KeyPress += TxtPrecio_KeyPress;
            btnBuscar.Enabled = false;
            mostrarTodoenData();

        }
        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
                        
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtID.Enabled = false;
                txtNombre.Enabled = true; txtNombre.Focus();
                e.Handled = true;
            }
        }
        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar == (char)Keys.Enter)
             {
                 e.Handled = true;
                 txtNombre.Enabled = false;
                 txtDetalles.Enabled = true;
                 txtDetalles.Focus(); 
             }
        }
        private void TxtDetalles_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            //{
            //    MessageBox.Show("Por favor, ingrese solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    e.Handled = true;
            //    txtDetalles.Focus();
            //}
            //else 
            //{
                if (e.KeyChar == (char)Keys.Enter)
                {
                    //cuando se cumple la condicion inabilita el texbox anterior a este y activa este
                    e.Handled = true;
                    txtDetalles.Enabled = false;
                    txtPrecio.Enabled = true; 
                    txtPrecio.Focus(); 
                }
            

           
        }
        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtPrecio.Enabled = false;
                btnGuardar.Enabled = true;
                btnGuardar.Focus(); 
            }
        }
        
        
        private void btnImagen_Click(object sender, EventArgs e)
            {

            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivo JPG|*.jpg|Archivo PNG|*.png|Archivo PDF|*.pdf";
            //abrir.Filter = "Archivo PNG | *.png";
            //abrir.Filter = "Archivo PDF | *.pdf";

            if (abrir.ShowDialog() == DialogResult.OK)
            {

                this.Imagen.Image = Bitmap.FromFile(abrir.FileName);
                this.Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

                txtID.Enabled = true;
                txtID.Focus();

            }

        }
        public void btnGuardar_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtID.Text);
            String Nombre = txtNombre.Text;
            double Precio = double.Parse(txtPrecio.Text);
            String Detalle = txtDetalles.Text;
            Image image = Imagen.Image;
            string ft = imagenserailizadaedicion;

 
            if (File.Exists(ruta))
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            //p10
            ArticulosLimpieza P = new ArticulosLimpieza(ID,Nombre,Detalle,Precio, image);
             bool agregarnodo = P.AgregarNodo(ruta, P, ref i); // aqui van el indice y la variable de control
             if (agregarnodo)
             {
               MessageBox.Show("Nodo Insertado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
 
            ConfiguracionDatagri(ID, Nombre,Detalle,Precio);


        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {

            // Cargar el archivo XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ruta);

            // Pedir al usuario que ingrese el nombre a buscar
            string idbuscado = txtID.Text;

            // Llamar a la función para buscar el nombre
            BuscarYMostrarDatos(xmlDoc, idbuscado);
        }
        
        
        
        public string ConfiguracionDatagri(int id, string name, string deta, double precio)
        {
            DataGridViewImageColumn ImagenColum = new DataGridViewImageColumn();

            foreach (DataGridViewColumn column in datagridview.Columns)
            {
                column.Width = 200; // Ajusta este valor según sea necesario

            }
            datagridview.RowTemplate.Height = 155;
            
            ImagenColum = (DataGridViewImageColumn)datagridview.Columns[4];
            ImagenColum.ImageLayout = DataGridViewImageCellLayout.Stretch;
            object[] fila1 = { id, name, deta, precio, Imagen.Image };
            datagridview.Rows.Add(fila1);

            return "";
        }
        private void BuscarYMostrarDatos(XmlDocument xmlDoc, string nombreBuscado)
        {
  
            XmlNodeList nodeList = xmlDoc.SelectNodes("//articulos");
            DataGridView DGV = new DataGridView();
            foreach (DataGridViewColumn column in datagridview.Columns)
            {
                column.Width = 200; // Ajusta este valor según sea necesario

            }
            datagridview.RowTemplate.Height = 155;
            // Iterar sobre los nodos para buscar el nombre
            bool encontrado = false;
            foreach (XmlNode node in nodeList)
            {
                XmlNode IDNode = node.SelectSingleNode("ID");
                
                if (IDNode != null && IDNode.InnerText == nombreBuscado)
                {
                    DataGridViewImageColumn ImagenColum = new DataGridViewImageColumn();
                    // El nombre fue encontrado, realizar acciones
                    encontrado = true;
                    MessageBox.Show("ID encontrado");

                    XmlNode NomNode = node.SelectSingleNode("Nombre");
                    XmlNode DetaNode = node.SelectSingleNode("Detalle");
                    XmlNode PreNode = node.SelectSingleNode("Precio");
                    XmlNode fotoNode = node.SelectSingleNode("Foto");

                    // Convertir la cadena base64 de la imagen a un objeto Image
                    byte[] imageBytes = Convert.FromBase64String(fotoNode.InnerText);
                    //Ayuda a desconvertir la imagen de la base64
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image imagenF = Image.FromStream(ms);
                        ImagenColum = (DataGridViewImageColumn)datagridview.Columns[4];
                        ImagenColum.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        //Ubicamos los ndos hijos en las columnas
                        object[] fila = { IDNode.InnerText, NomNode.InnerText, DetaNode.InnerXml, PreNode.InnerXml,imagenF };
                        datagridview.Rows.Add(fila);
                    }
                   
                    break;
                }
               
            }

            if (!encontrado)
            {
                MessageBox.Show("Nombre no encontrado en el archivo XML.");
            }
        }
        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Clear();txtDetalles.Clear();txtNombre.Clear();txtPrecio.Clear();btnGuardar.Enabled = false;Imagen.Image = null;
                txtID.Enabled = false; txtDetalles.Enabled = false; txtNombre.Enabled = false; txtPrecio.Enabled = false;
                datagridview.Rows.Clear();btnBuscar.Enabled = false;

                
            }
            catch
            {
                MessageBox.Show("Error: \n" + e.ToString());
            }


        }

        public void mostrarTodoenData() 
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ruta);
            XmlNodeList nodeList = xmlDoc.SelectNodes("//articulos");
            DataGridView DGV = new DataGridView();
            foreach (DataGridViewColumn column in datagridview.Columns)
            {
                column.Width = 200; // Ajusta este valor según sea necesario

            }
            datagridview.RowTemplate.Height = 155;

            foreach (XmlNode node in nodeList)
            {
                XmlNode IDNode = node.SelectSingleNode("ID");

                do
                {
                    DataGridViewImageColumn ImagenColum = new DataGridViewImageColumn();

                    XmlNode NomNode = node.SelectSingleNode("Nombre");
                    XmlNode DetaNode = node.SelectSingleNode("Detalle");
                    XmlNode PreNode = node.SelectSingleNode("Precio");
                    XmlNode fotoNode = node.SelectSingleNode("Foto");

                    // Convertir la cadena base64 de la imagen a un objeto Image
                    byte[] imageBytes = Convert.FromBase64String(fotoNode.InnerText);
                    //Ayuda a desconvertir la imagen de la base64
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image imagenF = Image.FromStream(ms);
                        ImagenColum = (DataGridViewImageColumn)datagridview.Columns[4];
                        ImagenColum.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        //Ubicamos los ndos hijos en las columnas
                        object[] fila = { IDNode.InnerText, NomNode.InnerText, DetaNode.InnerXml, PreNode.InnerXml, imagenF };
                        datagridview.Rows.Add(fila);
                    }
                    break;

                } while (nodeList.Count > 0 );
            } 
               
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTodo_Click(object sender, EventArgs e)
        {
            mostrarTodoenData();
            txtID.Enabled = true;
        }

        private void btnDesbloqueo_Click(object sender, EventArgs e)
        {
            btnBuscar.Enabled = true;txtID.Enabled = true;
        }
    }
}
