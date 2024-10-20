using System;
using System.Collections.Generic;
using System.Drawing; //Agregar
using System.IO; //Agregar
using System.Drawing.Imaging; //Agregar
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; //Agregar
using System.Xml; //Agregar

namespace GenerarArchivoXML
{
    public class ArticulosLimpieza
    {
        //Paso 4
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Precio { get; set; }
        public string Foto { get; set; }

        //Paso 5
        public ArticulosLimpieza(int id, string nombre, string detalle, double precio, Image foto) 
        {

            this.ID = id;
            this.Nombre = nombre;
            this.Detalle = detalle;
            this.Precio = precio;
            this.Foto = SFoto(foto);

        }

        public ArticulosLimpieza()
        {

        }

        // Paso 7
        public string SFoto(Image image)
        {

            if (image != null)
            {

                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());

            }

            return null;

        }

        // Paso 8
        public bool AgregarNodo(string Ruta, ArticulosLimpieza articuloslimpieza, ref int i)
        {
            try
            {
                if (i == 0) // si no hay archivo crearlo, sino abrir
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Raiz));

                    // archivo nuevo
                    using (TextWriter TW = new StreamWriter(Ruta))
                    {
                        // crear objeto Raiz
                        Raiz nodoRaiz = new Raiz
                        {
                            articulos = articuloslimpieza
                        };

                        // Agregar el nodo raiz al archivo
                        xmlSerializer.Serialize(TW, nodoRaiz);
                    }
                }
                else
                {
                    // abrir  archivo xml
                    XmlDocument doc = new XmlDocument();
                    // cargar el archivo
                    doc.Load(Ruta);
                    XmlNode nodoRaiz = doc.DocumentElement;
                    // colocar el nodo del articulo

                    // CREACION DEL NODO PRODUCTO
                    XmlNode nodoProducto = doc.CreateElement("articulos");

                    // ID
                    XmlElement IDxml = doc.CreateElement("ID");
                    IDxml.InnerText = articuloslimpieza.ID.ToString();
                    nodoProducto.AppendChild(IDxml);

                    // NOMB
                    XmlElement NOMBxml = doc.CreateElement("Nombre");
                    NOMBxml.InnerText = articuloslimpieza.Nombre.ToString();
                    nodoProducto.AppendChild(NOMBxml);

                    XmlElement DESCxml = doc.CreateElement("Detalle");
                    DESCxml.InnerText = articuloslimpieza.Detalle.ToString();
                    nodoProducto.AppendChild(DESCxml);

                    // DESC
                    XmlElement PRCxml = doc.CreateElement("Precio");
                    PRCxml.InnerText = articuloslimpieza.Precio.ToString();
                    nodoProducto.AppendChild(PRCxml);

                    // FT
                    XmlElement FTxml = doc.CreateElement("Foto");
                    FTxml.InnerText = articuloslimpieza.Foto.ToString();
                    nodoProducto.AppendChild(FTxml);

                    nodoRaiz.InsertAfter(nodoProducto, nodoRaiz.LastChild);
                    doc.Save(Ruta);

                }
            }
            catch (Exception e)
            {

                return false;
            }
            return true;

        }
    }
}
