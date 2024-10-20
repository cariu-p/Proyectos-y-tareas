using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GenerarArchivoXML
{
    //Paso 3
    [XmlRoot(ElementName = "Raiz")]
    public class Raiz
    {

        public Raiz() { }

        public ArticulosLimpieza articulos;

    }
}
