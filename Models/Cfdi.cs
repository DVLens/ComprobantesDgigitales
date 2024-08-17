using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace XMLCfdiGenerator.Models
{
    /// <summary>
    /// Llenado CFDI 4.0 Clase derivada con todos los campos representados en un comprobante digital.
    /// El llenado de los mismos puede ser consultado en: http://omawww.sat.gob.mx/tramitesyservicios/Paginas/documentos/Anexo_20_Guia_de_llenado_CFDI.pdf
    /// </summary> 
    public class CFDI
    {
        /// <summary>
        /// Nodo principal que engloba todos los datos del comprobante.
        /// </summary>
        public required NodoComprobante nodoComprobante {get; set;}
    }
    /// <summary>
    /// Elementos del nodo comprobante.
    /// </summary>
    [XmlRoot(ElementName = "cfdi:Comprobante", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoComprobante
    {
        /// <summary>
        /// Atributo requerido con valor prefijado a 4.0 que indica la versión del estándar bajo el que se encuentra expresado el comprobante. 
        /// </summary>
        /// <example>4.0</example>
        [XmlAttribute(AttributeName = "Version")]
        [Required(ErrorMessage = "La version es un campo obligatorio.")]
        public string Version { get; set; } = "4.0";
        /// <summary>
        /// Atributo opcional para precisar la serie para control interno del contribuyente. Este atributo acepta una cadena de caracteres.
        /// </summary>
        [XmlAttribute(AttributeName = "Serie")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "La longitud de la serie debe estar entre 1 y 25 caracteres.")]
        [RegularExpression(@"[^|]{1,25}", ErrorMessage = "La serie no debe contener el carácter '|'.")]
        public string? Serie { get; set; }
        /// <summary>
        /// Atributo opcional para control interno del contribuyente que expresa el folio del comprobante, acepta una cadena de caracteres.
        /// </summary>
        [XmlAttribute(AttributeName = "Folio")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "La longitud del folio debe estar entre 1 y 40 caracteres.")]
        [RegularExpression(@"[^|]{1,40}", ErrorMessage = "El folio no debe contener el carácter '|'.")]
        public string? Folio { get; set; }
        /// <summary>
        /// Atributo requerido para la expresión de la fecha y hora de expedición del Comprobante Fiscal Digital por Internet. Se expresa en la forma AAAA-MM-DDThh:mm:ss y debe corresponder con la hora local donde se expide el comprobante
        /// </summary>
        [XmlAttribute(AttributeName = "Fecha")]
        [Required(ErrorMessage = "La Fecha es un campo obligatorio.")]
        [DataType(DataType.DateTime, ErrorMessage = "La fecha debe estar en el formato AAAA-MM-DDThh:mm:ss.")]
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Atributo requerido para contener el sello digital del comprobante fiscal, al que hacen referencia las reglas de resolución miscelánea vigente. El sello debe ser expresado como una cadena de texto en formato Base 64.
        /// </summary>
        [XmlAttribute(AttributeName = "Sello")]
        [Required(ErrorMessage = "El Sello es un campo obligatorio.")]
        public string? Sello { get; set; }
        /// <summary>
        /// Atributo condicional para expresar la clave de la forma de pago de los bienes o servicios amparados por el comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "FormaPago")]
        public string? FormaPago { get; set; }
        /// <summary>
        /// Atributo requerido para expresar el número de serie del certificado de sello digital que ampara al comprobante, de acuerdo con el acuse correspondiente a 20 posiciones otorgado por el sistema del SAT.
        /// </summary>
        [XmlAttribute(AttributeName = "NoCertificado")]
        [Required(ErrorMessage = "El NoCertificado es un campo obligatorio.")]
        public string? NoCertificado { get; set; }
        /// <summary>
        /// Atributo requerido que sirve para incorporar el certificado de sello digital que ampara al comprobante, como texto en formato base 64.
        /// </summary>
        [XmlAttribute(AttributeName = "Certificado")]
        [Required(ErrorMessage = "El Certificado es un campo obligatorio.")]
        public string? Certificado { get; set; }
        /// <summary>
        /// Atributo condicional para expresar las condiciones comerciales aplicables para el pago del comprobante fiscal digital por Internet. Este atributo puede ser condicionado mediante atributos o complementos.
        /// </summary>
        [XmlAttribute(AttributeName = "CondicionesDePago")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "La longitud de las condiciones de pago debe estar entre 1 y 1000 caracteres.")]
        [RegularExpression(@"[^|]{1,1000}", ErrorMessage = "Las condiciones de pago no deben contener el carácter '|'.")]
        public string? CondicionesDePago { get; set; }
        /// <summary>
        /// Atributo requerido para representar la suma de los importes de los conceptos antes de descuentos e impuesto. No se permiten valores negativos.
        /// </summary>
        [XmlAttribute(AttributeName = "SubTotal")]
        [Required(ErrorMessage = "El subtotal es un campo obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El subtotal no puede ser un valor negativo.")]
        public decimal SubTotal { get; set; }
        /// <summary>
        /// Atributo requerido para representar la suma de los importes de los conceptos antes de descuentos e impuesto. No se permiten valores negativos.
        /// </summary>
        [XmlAttribute(AttributeName = "Descuento")]
        public decimal? Descuento { get; set; }
        /// <summary>
        /// Atributo requerido para identificar la clave de la moneda utilizada para expresar los montos, cuando se usa moneda nacional se registra MXN. Conforme con la especificación ISO 4217.
        /// </summary>
        [XmlAttribute(AttributeName = "Moneda")]
        [Required(ErrorMessage = "La Moneda es un campo obligatorio.")]
        public string? Moneda { get; set; }
        /// <summary>
        /// Atributo condicional para representar el tipo de cambio conforme con la moneda usada. Es requerido cuando la clave de moneda es distinta de MXN y de XXX. El valor debe reflejar el número de pesos mexicanos que equivalen a una unidad de la divisa señalada en el atributo moneda. Si el valor está fuera del porcentaje aplicable a la moneda tomado del catálogo c_Moneda, el emisor debe obtener del PAC que vaya a timbrar el CFDI, de manera no automática, una clave de confirmación para ratificar que el valor es correcto e integrar dicha clave en el atributo Confirmacion.
        /// </summary>
        [XmlAttribute(AttributeName = "TipoCambio")]
        public string? TipoCambio { get; set; }
        /// <summary>
        /// Atributo requerido para representar la suma del subtotal, menos los descuentos aplicables, más las contribuciones recibidas (impuestos trasladados - federales o locales, derechos, productos, aprovechamientos, aportaciones de seguridad social, contribuciones de mejoras) menos los impuestos retenidos. Si el valor es superior al límite que establezca el SAT en la Resolución Miscelánea Fiscal vigente, el emisor debe obtener del PAC que vaya a timbrar el CFDI, de manera no automática, una clave de confirmación para ratificar que el valor es correcto e integrar dicha clave en el atributo Confirmacion. No se permiten valores negativos.
        /// </summary>
        [XmlAttribute(AttributeName = "Total")]
        [Required(ErrorMessage = "El Total es un campo obligatorio.")]
        public decimal? Total { get; set; }
        /// <summary>
        /// Atributo requerido para expresar la clave del efecto del comprobante fiscal para el contribuyente emisor.
        /// </summary>
        [XmlAttribute(AttributeName = "TipoDeComprobante")]
        [Required(ErrorMessage = "El TipoDeComprobante es un campo obligatorio.")]
        public string? TipoDeComprobante { get; set; }
        /// <summary>
        /// Atributo requerido para expresar si el comprobante ampara una operación de exportación.
        /// </summary>
        [XmlAttribute(AttributeName = "Exportacion")]
        [Required(ErrorMessage = "La Exportacion es un campo obligatorio.")]
        public string? Exportacion { get; set; }
        /// <summary>
        /// Atributo condicional para precisar la clave del método de pago que aplica para este comprobante fiscal digital por Internet, conforme al Artículo 29-A fracción VII incisos a y b del CFF.
        /// </summary>
        [XmlAttribute(AttributeName = "MetodoPago")]
        public string? MetodoPago { get; set; }
        /// <summary>
        /// Atributo requerido para incorporar el código postal del lugar de expedición del comprobante (domicilio de la matriz o de la sucursal).
        /// </summary>
        [XmlAttribute(AttributeName = "LugarExpedicion")]
        [Required(ErrorMessage = "El LugarExpedicion es un campo obligatorio.")]
        public string LugarExpedicion { get; set; }
        /// <summary>
        /// Atributo condicional para registrar la clave de confirmación que entregue el PAC para expedir el comprobante con importes grandes, con un tipo de cambio fuera del rango establecido o con ambos casos. Es requerido cuando se registra un tipo de cambio o un total fuera del rango establecido.
        /// </summary>
        [XmlAttribute(AttributeName = "Confirmacion")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "La Confirmación debe tener exactamente 5 caracteres.")]
        [RegularExpression(@"[0-9a-zA-Z]{5}", ErrorMessage = "La Confirmación debe contener solo caracteres alfanuméricos.")]
        public string? Confirmacion { get; set; }
        
        public NodoEmisor Emisor { get; set; }
        public required NodoReceptor Receptor { get; set; }
        public required NodoConceptos Conceptos { get; set; }

        public required NodoImpuestos Impuestos { get; set; }
        
        public required NodoComplemento Complemento { get; set; }

    }
    /// <summary>
    /// Nodo requerido para expresar la información del contribuyente emisor del comprobante.
    /// </summary>
    [XmlRoot(ElementName = "cfdi:Emisor", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoEmisor
    {
        /// <summary>
        /// Atributo requerido para registrar la Clave del Registro Federal de Contribuyentes correspondiente al contribuyente emisor del comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "Rfc")]        
        [Required(ErrorMessage = "El RFC es un campo obligatorio.")]
        [RegularExpression(@"[A-ZÑ&]{3,4}[0-9]{6}[A-Z0-9]{3}", ErrorMessage = "El RFC debe cumplir con el formato requerido.")]
        public string Rfc { get; set; }
        /// <summary>
        /// Atributo opcional para registrar el nombre, denominación o razón social del contribuyente emisor del comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "Nombre")]
        [Required(ErrorMessage = "El nombre es un campo obligatorio.")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "La longitud del nombre debe estar entre 1 y 300 caracteres.")]
        [RegularExpression(@"[^|]{1,300}", ErrorMessage = "El nombre no debe contener el carácter '|'.")]
        public string? Nombre { get; set; }
        /// <summary>
        /// Atributo requerido para incorporar la clave del régimen del contribuyente emisor al que aplicará el efecto fiscal de este comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "RegimenFiscal")]
        [Required(ErrorMessage = "El régimen fiscal es un campo obligatorio.")]
        public string RegimenFiscal { get; set; }
    }

    /// <summary>
    /// Nodo requerido para precisar la información del contribuyente receptor del comprobante.
    /// </summary>
    [XmlRoot(ElementName = "cfdi:Receptor", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoReceptor
    {
        /// <summary>
        /// Atributo requerido para precisar la Clave del Registro Federal de Contribuyentes correspondiente al contribuyente receptor del comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "Rfc")]
        public required string Rfc { get; set; }
        /// <summary>
        /// Atributo opcional para precisar el nombre, denominación o razón social del contribuyente receptor del comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "Nombre")]
        public string? Nombre { get; set; }
        /// <summary>
        /// Atributo condicional para registrar la clave del país de residencia para efectos fiscales del receptor del comprobante, cuando se trate de un extranjero, y que es conforme con la especificación ISO 3166-1 alpha-3. Es requerido cuando se incluya el complemento de comercio exterior o se registre el atributo NumRegIdTrib.
        /// </summary>
        [XmlAttribute(AttributeName = "ResidenciaFiscal")]
        public string? ResidenciaFiscal { get; set; }
        /// <summary>
        /// Atributo condicional para expresar el número de registro de identidad fiscal del receptor cuando sea residente en el extranjero. Es requerido cuando se incluya el complemento de comercio exterior.
        /// </summary>
        [XmlAttribute(AttributeName = "NumRegIdTrib")]
        public string? NumRegIdTrib { get; set; }

        [XmlAttribute(AttributeName = "RegimenFiscalReceptor")]
        public string RegimenFiscalReceptor { get; set; }
        [XmlAttribute(AttributeName = "DomicilioFiscalReceptor")]
        public string DomicilioFiscalReceptor { get; set; }
        /// <summary>
        /// Atributo requerido para expresar la clave del uso que dará a esta factura el receptor del CFDI.
        /// </summary>
        [XmlAttribute(AttributeName = "UsoCFDI")]
        public required string UsoCFDI { get; set; }
    }
    [XmlRoot(ElementName = "cfdi:Conceptos", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoConceptos {
        public List<NodoConcepto> ListaConceptos;
    }
    [XmlRoot(ElementName = "cfdi:Concepto", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoConcepto
    {
        [XmlAttribute(AttributeName = "ClaveProdServ")]
        public string ClaveProdServ { get; set; }
        [XmlAttribute(AttributeName = "ClaveUnidad")]
        public string ClaveUnidad { get; set; }
        [XmlAttribute(AttributeName = "Cantidad")]
        public string Cantidad { get; set; }
        [XmlAttribute(AttributeName = "Unidad")]
        public string Unidad { get; set; }
        [XmlAttribute(AttributeName = "NoIdentificacion")]
        public string NoIdentificacion { get; set; }
        [XmlAttribute(AttributeName = "Descripcion")]
        public string Descripcion { get; set; }
        [XmlAttribute(AttributeName = "ObjetoImp")]
        public string ObjetoImp { get; set; }
        [XmlAttribute(AttributeName = "ValorUnitario")]
        public string ValorUnitario { get; set; }
        [XmlAttribute(AttributeName = "Importe")]
        public string Importe { get; set; }

        public List<NodoTraslados> Traslados { get; set;}

    }
    [XmlRoot(ElementName = "cfdi:Traslados", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoTraslados
    {
        public List<NodoTraslado> ListaTraslados;
    }
    [XmlRoot(ElementName = "cfdi:Traslado", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoTraslado
    {
        [XmlAttribute(AttributeName = "Base")]
        public decimal Base { get; set; }
        [XmlAttribute(AttributeName = "Impuesto")]
        public string Impuesto { get; set; }
        [XmlAttribute(AttributeName = "TipoFactor")]
        public string TipoFactor { get; set; }
        [XmlAttribute(AttributeName = "TasaOCuota")]
        public decimal TasaOCuota { get; set; }
        [XmlAttribute(AttributeName = "Importe")]
        public decimal Importe { get; set; }
    }
    [XmlRoot(ElementName = "cfdi:Impuestos", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoImpuestos
    {
        [XmlAttribute(AttributeName = "TotalImpuestosTrasladados")]
        public decimal TotalImpuestosTrasladados { get; set; }
        public List<NodoTraslados> ListaTraslados { get; set; }
    }
    [XmlRoot(ElementName = "cfdi:Complemento", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoComplemento
    {
        [XmlAttribute(AttributeName = "TotalImpuestosTrasladados")]
        public decimal TotalImpuestosTrasladados { get; set; }
        public List<NodoTimbreFiscalDigital> NodoTimbreFiscalDigital { get; set; }
    }

    [XmlRoot(ElementName = "tfd:TimbreFiscalDigital ", Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
    public class NodoTimbreFiscalDigital
    {
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "UUID")]
        public string UUID { get; set; }
        [XmlAttribute(AttributeName = "FechaTimbrado")]
        public string FechaTimbrado { get; set; }
        [XmlAttribute(AttributeName = "RfcProvCertif")]
        public string RfcProvCertif { get; set; }
        [XmlAttribute(AttributeName = "SelloCFD")]
        public string SelloCFD { get; set; }
        [XmlAttribute(AttributeName = "NoCertificadoSAT")]
        public string NoCertificadoSAT { get; set; }
        [XmlAttribute(AttributeName = "SelloSAT")]
        public string SelloSAT { get; set; }

    }
}
