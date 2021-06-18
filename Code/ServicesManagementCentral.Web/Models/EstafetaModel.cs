using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class ResponseModels
    {

        public string Cve_RespCode { get; set; } = "00";
        public string Guia { get; set; } = "";
        public string Desc_MensajeError { get; set; } = "";
        public byte[] pdf { get; set; }
        //public string cotizacion { get; set; }
        public Respuesta cotizacion { get; set; }

    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class TipoEnvio
    {

        private bool esPaqueteField;

        private double largoField;

        private double pesoField;

        private double altoField;

        private double anchoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public bool EsPaquete
        {
            get
            {
                return this.esPaqueteField;
            }
            set
            {
                this.esPaqueteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public double Largo
        {
            get
            {
                return this.largoField;
            }
            set
            {
                this.largoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public double Peso
        {
            get
            {
                return this.pesoField;
            }
            set
            {
                this.pesoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public double Alto
        {
            get
            {
                return this.altoField;
            }
            set
            {
                this.altoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public double Ancho
        {
            get
            {
                return this.anchoField;
            }
            set
            {
                this.anchoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class Origen
    {

        private string codigoPosOriField;

        private string plazaOriField;

        private string municipioOriField;

        private string estadoOriField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string CodigoPosOri
        {
            get
            {
                return this.codigoPosOriField;
            }
            set
            {
                this.codigoPosOriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string PlazaOri
        {
            get
            {
                return this.plazaOriField;
            }
            set
            {
                this.plazaOriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string MunicipioOri
        {
            get
            {
                return this.municipioOriField;
            }
            set
            {
                this.municipioOriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string EstadoOri
        {
            get
            {
                return this.estadoOriField;
            }
            set
            {
                this.estadoOriField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class Destino
    {

        private string cpDestinoField;

        private string plaza1Field;

        private string municipioField;

        private string estadoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string CpDestino
        {
            get
            {
                return this.cpDestinoField;
            }
            set
            {
                this.cpDestinoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Plaza1
        {
            get
            {
                return this.plaza1Field;
            }
            set
            {
                this.plaza1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Estado
        {
            get
            {
                return this.estadoField;
            }
            set
            {
                this.estadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class DiasEntrega
    {

        private string lunesField;

        private string martesField;

        private string miercolesField;

        private string juevesField;

        private string viernesField;

        private string sabadoField;

        private string domingoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Lunes
        {
            get
            {
                return this.lunesField;
            }
            set
            {
                this.lunesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Martes
        {
            get
            {
                return this.martesField;
            }
            set
            {
                this.martesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Miercoles
        {
            get
            {
                return this.miercolesField;
            }
            set
            {
                this.miercolesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Jueves
        {
            get
            {
                return this.juevesField;
            }
            set
            {
                this.juevesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Viernes
        {
            get
            {
                return this.viernesField;
            }
            set
            {
                this.viernesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string Sabado
        {
            get
            {
                return this.sabadoField;
            }
            set
            {
                this.sabadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Domingo
        {
            get
            {
                return this.domingoField;
            }
            set
            {
                this.domingoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class ModalidadEntrega
    {

        private string ocurreForzosoField;

        private string frecuenciaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string OcurreForzoso
        {
            get
            {
                return this.ocurreForzosoField;
            }
            set
            {
                this.ocurreForzosoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Frecuencia
        {
            get
            {
                return this.frecuenciaField;
            }
            set
            {
                this.frecuenciaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class Colonia
    {

        private string[] coloniasField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
        public string[] Colonias
        {
            get
            {
                return this.coloniasField;
            }
            set
            {
                this.coloniasField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class TipoServicio
    {

        private string descripcionServicioField;

        private int tipoEnvioResField;

        private string aplicaCotizacionField;

        private double tarifaBaseField;

        private double cCTarifaBaseField;

        private double cargosExtraField;

        private double sobrePesoField;

        private double cCSobrePesoField;

        private double costoTotalField;

        private double pesoField;

        private string aplicaServicioField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string DescripcionServicio
        {
            get
            {
                return this.descripcionServicioField;
            }
            set
            {
                this.descripcionServicioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int TipoEnvioRes
        {
            get
            {
                return this.tipoEnvioResField;
            }
            set
            {
                this.tipoEnvioResField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string AplicaCotizacion
        {
            get
            {
                return this.aplicaCotizacionField;
            }
            set
            {
                this.aplicaCotizacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public double TarifaBase
        {
            get
            {
                return this.tarifaBaseField;
            }
            set
            {
                this.tarifaBaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public double CCTarifaBase
        {
            get
            {
                return this.cCTarifaBaseField;
            }
            set
            {
                this.cCTarifaBaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public double CargosExtra
        {
            get
            {
                return this.cargosExtraField;
            }
            set
            {
                this.cargosExtraField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public double SobrePeso
        {
            get
            {
                return this.sobrePesoField;
            }
            set
            {
                this.sobrePesoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public double CCSobrePeso
        {
            get
            {
                return this.cCSobrePesoField;
            }
            set
            {
                this.cCSobrePesoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public double CostoTotal
        {
            get
            {
                return this.costoTotalField;
            }
            set
            {
                this.costoTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public double Peso
        {
            get
            {
                return this.pesoField;
            }
            set
            {
                this.pesoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string AplicaServicio
        {
            get
            {
                return this.aplicaServicioField;
            }
            set
            {
                this.aplicaServicioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.estafeta.com/")]
    public partial class Respuesta
    {

        private TipoEnvio tipoEnvioField;

        private TipoServicio[] tipoServicioField;

        private Colonia coloniasField;

        private ModalidadEntrega modalidadEntregaField;

        private DiasEntrega diasEntregaField;

        private string costoReexpedicionField;

        private string existenteSiglaOriField;

        private string existenteSiglaDesField;

        private Destino destinoField;

        private Origen origenField;

        private string errorField;

        private string mensajeErrorField;

        private string codigoPosOriField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public TipoEnvio TipoEnvio
        {
            get
            {
                return this.tipoEnvioField;
            }
            set
            {
                this.tipoEnvioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order = 1)]
        public TipoServicio[] TipoServicio
        {
            get
            {
                return this.tipoServicioField;
            }
            set
            {
                this.tipoServicioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public Colonia Colonias
        {
            get
            {
                return this.coloniasField;
            }
            set
            {
                this.coloniasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public ModalidadEntrega ModalidadEntrega
        {
            get
            {
                return this.modalidadEntregaField;
            }
            set
            {
                this.modalidadEntregaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public DiasEntrega DiasEntrega
        {
            get
            {
                return this.diasEntregaField;
            }
            set
            {
                this.diasEntregaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string CostoReexpedicion
        {
            get
            {
                return this.costoReexpedicionField;
            }
            set
            {
                this.costoReexpedicionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string ExistenteSiglaOri
        {
            get
            {
                return this.existenteSiglaOriField;
            }
            set
            {
                this.existenteSiglaOriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string ExistenteSiglaDes
        {
            get
            {
                return this.existenteSiglaDesField;
            }
            set
            {
                this.existenteSiglaDesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public Destino Destino
        {
            get
            {
                return this.destinoField;
            }
            set
            {
                this.destinoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public Origen Origen
        {
            get
            {
                return this.origenField;
            }
            set
            {
                this.origenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string Error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string MensajeError
        {
            get
            {
                return this.mensajeErrorField;
            }
            set
            {
                this.mensajeErrorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string CodigoPosOri
        {
            get
            {
                return this.codigoPosOriField;
            }
            set
            {
                this.codigoPosOriField = value;
            }
        }
    }

}