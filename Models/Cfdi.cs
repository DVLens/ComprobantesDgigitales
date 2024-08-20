using System.ComponentModel.DataAnnotations;
using System.Xml;
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
        public required NodoComprobante nodoComprobante { get; set; }
    }
    /// <summary>
    /// Estándar de Comprobante Fiscal Digital por Internet.
    /// </summary>
    [XmlRoot(ElementName = "Comprobante", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoComprobante
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces(new[]
    {
        new XmlQualifiedName("cfdi", "http://www.sat.gob.mx/cfd/4"),
        new XmlQualifiedName("xs", "http://www.w3.org/2001/XMLSchema"),
        new XmlQualifiedName("catCFDI", "http://www.sat.gob.mx/sitio_internet/cfd/catalogos"),
        new XmlQualifiedName("tdCFDI", "http://www.sat.gob.mx/sitio_internet/cfd/tipoDatos/tdCFDI")
    });

        public NodoInformacionGlobal? InformacionGlobal { get; set; }
        public NodoCfdisRelacionados? CfdiRelacionados { get; set; }
        [Required(ErrorMessage = "El comprobante debe contar con el nodo emisor.")]
        public NodoEmisor Emisor { get; set; }
        [Required(ErrorMessage = "El comprobante debe contar con el nodo receptor.")]
        public NodoReceptor Receptor { get; set; }
        [Required(ErrorMessage = "El comprobante debe contar con almenos una lista de conceptos.")]
        public NodoConceptos Conceptos { get; set; }

        public required NodoImpuestos Impuestos { get; set; }

        public required NodoComplemento Complemento { get; set; }
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
    }
    /// <summary>
    /// Nodo condicional para precisar la información relacionada con el comprobante global.
    /// </summary>
    [XmlRoot(ElementName = "InformacionGlobal", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoInformacionGlobal
    {
        /// <summary>
        /// Atributo requerido para expresar el período al que corresponde la información del comprobante global.
        /// </summary>
        [XmlAttribute(AttributeName = "Periodicidad")]
        [Required(ErrorMessage = "La periodicidad es requerida.")]
        public string Periodicidad { get; set; }
        /// <summary>
        /// Atributo requerido para expresar el mes o los meses al que corresponde la información del comprobante global.
        /// </summary>
        [XmlAttribute(AttributeName = "Meses")]
        [Required(ErrorMessage = "El mes o los meses son requeridos.")]
        public string Meses { get; set; }
        /// <summary>
        /// Atributo requerido para expresar el año al que corresponde la información del comprobante global.
        /// </summary>
        [XmlAttribute(AttributeName = "Año")]
        [Required(ErrorMessage = "El año es requerido.")]
        [Range(2019, short.MaxValue, ErrorMessage = "El año debe ser 2019 o posterior.")]
        public short Año { get; set; }
    }
    /// <summary>
    /// Nodo opcional para precisar la información de los comprobantes relacionados.
    /// </summary>
    public class NodoCfdisRelacionados
    {
        /// <summary>
        /// Lista de nodos de Cfdis relacionados.
        /// </summary>
        public List<NodoCfdiRelacionado> CfdiRelacionados { get; set; }
    }
    /// <summary>
    /// Nodo requerido para precisar la información de los comprobantes relacionados.
    /// </summary>
    public class NodoCfdiRelacionado
    {
        /// <summary>
        /// Atributo requerido para registrar el folio fiscal (UUID) de un CFDI relacionado con el presente comprobante, por ejemplo: Si el CFDI relacionado es un comprobante de traslado que sirve para registrar el movimiento de la mercancía. Si este comprobante se usa como nota de crédito o nota de débito del comprobante relacionado. Si este comprobante es una devolución sobre el comprobante relacionado. Si éste sustituye a una factura cancelada.
        /// </summary>
        [XmlAttribute(AttributeName = "UUID")]
        [Required(ErrorMessage = "El UUID es requerido.")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "El UUID debe tener exactamente 36 caracteres.")]
        [RegularExpression(@"[a-f0-9A-F]{8}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{12}", ErrorMessage = "El UUID debe seguir el formato XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX.")]
        public string UUID { get; set; }
        /// <summary>
        /// Atributo requerido para indicar la clave de la relación que existe entre éste que se está generando y el o los CFDI previos.
        /// </summary>
        [XmlAttribute(AttributeName = "TipoRelacion")]
        [Required(ErrorMessage = "El tipo de relación es requerido.")]
        public string TipoRelacion { get; set; }
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
        /// <summary>
        /// Atributo requerido para incorporar la clave del régimen fiscal del contribuyente receptor al que aplicará el efecto fiscal de este comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "RegimenFiscalReceptor")]
        public string RegimenFiscalReceptor { get; set; }
        /// <summary>
        /// Atributo requerido para registrar el código postal del domicilio fiscal del receptor del comprobante.
        /// </summary>
        [XmlAttribute(AttributeName = "DomicilioFiscalReceptor")]
        public string DomicilioFiscalReceptor { get; set; }
        /// <summary>
        /// Atributo requerido para expresar la clave del uso que dará a esta factura el receptor del CFDI.
        /// </summary>
        [XmlAttribute(AttributeName = "UsoCFDI")]
        public required string UsoCFDI { get; set; }
    }
    [XmlRoot(ElementName = "cfdi:Conceptos", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoConceptos
    {
        public List<NodoConcepto> Conceptos { get; set; }
    }
    [XmlRoot(ElementName = "cfdi:Concepto", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoConcepto
    {
        /// <summary>
        /// Nodo condicional para capturar los impuestos aplicables al presente concepto.
        /// </summary>
        public List<NodoImpuestos>? Impuestos { get; set; }

        public NodoACuentaTerceros ACuentaTerceros { get; set; }

        public InformacionAduanera InformacionAduanera { get; set; }
        public CuentaPredial CuentaPredial { get; set; }
        public ComplementoConcepto ComplementoConcepto { get; set; }
        /// <summary>
        /// Atributo requerido para expresar la clave del producto o del servicio amparado por la presente parte. Es requerido y deben utilizar las claves del catálogo de productos y servicios, cuando los conceptos que registren por sus actividades correspondan con dichos conceptos.
        /// </summary>
        [XmlAttribute(AttributeName = "ClaveProdServ")]
        public string? ClaveProdServ { get; set; }
        /// <summary>
        /// Atributo opcional para expresar el número de serie, número de parte del bien o identificador del producto o del servicio amparado por la presente parte. Opcionalmente se puede utilizar claves del estándar GTIN.
        /// </summary>
        [XmlAttribute(AttributeName = "NoIdentificacion")]
        [RegularExpression(@"[^|]{1,100}", ErrorMessage = "El NoIdentificacion no debe contener el carácter '|' y debe tener entre 1 y 100 caracteres.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El NoIdentificacion debe tener entre 1 y 100 caracteres.")]
        public string? NoIdentificacion { get; set; }
        /// <summary>
        /// Atributo requerido para precisar la cantidad de bienes o servicios del tipo particular definido por la presente parte.
        /// </summary>
        [XmlAttribute(AttributeName = "Cantidad")]
        [Required(ErrorMessage = "El atributo Cantidad es requerido.")]
        [Range(typeof(decimal), "0.000001", "79228162514264337593543950335", ErrorMessage = "La Cantidad debe ser un valor decimal mayor o igual a 0.000001.")]
        [RegularExpression(@"^\d+(\.\d{1,6})?$", ErrorMessage = "La Cantidad debe tener hasta 6 dígitos decimales.")]
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Atributo requerido para precisar la clave de unidad de medida estandarizada aplicable para la cantidad expresada en el concepto. La unidad debe corresponder con la descripción del concepto.
        /// </summary>
        [XmlAttribute(AttributeName = "ClaveUnidad")]
        [Required(ErrorMessage = "El atributo ClaveUnidad es requerido.")]
        public string ClaveUnidad { get; set; }
        /// <summary>
        /// Atributo requerido para precisar la descripción del bien o servicio cubierto por la presente parte.
        /// </summary>
        [XmlAttribute(AttributeName = "Descripcion")]
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "La descripción debe tener entre 1 y 1000 caracteres.")]
        [RegularExpression(@"[^|]{1,1000}", ErrorMessage = "La descripción no debe contener el carácter '|'.")]
        public string Descripcion { get; set; }
        /// <summary>
        /// Atributo opcional para precisar la unidad de medida propia de la operación del emisor, aplicable para la cantidad expresada en el concepto. La unidad debe corresponder con la descripción del concepto.
        /// </summary>
        [XmlAttribute(AttributeName = "Unidad")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "La unidad debe tener entre 1 y 20 caracteres.")]
        [RegularExpression(@"[^|]{1,20}", ErrorMessage = "La unidad no debe contener el carácter '|'.")]
        public string Unidad { get; set; }
        /// <summary>
        /// Atributo requerido para precisar el valor o precio unitario del bien o servicio cubierto por el presente concepto.
        /// </summary>
        [XmlAttribute(AttributeName = "ValorUnitario")]
        [Required(ErrorMessage = "El valor unitario es requerido.")]
        public decimal ValorUnitario { get; set; }
        /// <summary>
        /// Atributo requerido para expresar si la operación comercial es objeto o no de impuesto.
        /// </summary>
        [XmlAttribute(AttributeName = "ObjetoImp")]
        [Required(ErrorMessage = "El objeto importe es requerido.")]
        public string ObjetoImp { get; set; }
        /// <summary>
        /// Atributo requerido para precisar el importe total de los bienes o servicios del presente concepto. Debe ser equivalente al resultado de multiplicar la cantidad por el valor unitario expresado en el concepto. No se permiten valores negativos. 
        /// </summary>
        [XmlAttribute(AttributeName = "Importe")]
        public decimal Importe { get; set; }
        /// <summary>
        /// Atributo opcional para representar el importe de los descuentos aplicables al concepto. No se permiten valores negativos.
        /// </summary>
        [XmlAttribute(AttributeName = "Descuento")]
        [Range(0, double.MaxValue, ErrorMessage = "El descuento no puede ser negativo.")]
        public decimal? Descuento { get; set; }
    }

    [XmlRoot(ElementName = "CuentaPredial")]
    public class CuentaPredial
    {
        [XmlAttribute(AttributeName = "Numero")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "El número de cuenta predial debe tener entre 1 y 150 caracteres.")]
        [RegularExpression(@"[0-9a-zA-Z]{1,150}", ErrorMessage = "El número de cuenta predial solo puede contener caracteres alfanuméricos.")]
        public string Numero { get; set; }
    }
    [XmlRoot(ElementName = "ComplementoConcepto")]
    public class ComplementoConcepto
    {
        [XmlAnyElement]
        public List<XmlElement> Any { get; set; }
    }


    [XmlRoot(ElementName = "InformacionAduanera")]
    public class InformacionAduanera
    {
        [XmlAttribute(AttributeName = "NumeroPedimento")]
        [StringLength(21, ErrorMessage = "El número de pedimento debe tener 21 caracteres.")]
        [RegularExpression(@"[0-9]{2} [0-9]{2} [0-9]{4} [0-9]{7}", ErrorMessage = "El formato del número de pedimento es incorrecto.")]
        public string NumeroPedimento { get; set; }
    }

    [XmlRoot(ElementName = "cfdi:Traslados", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoTraslados
    {
        public List<NodoTraslado> ListaTraslados;
    }

    [XmlRoot(ElementName = "Traslado")]
    public class NodoTraslado
    {
        [XmlAttribute(AttributeName = "Base")]
        [Range(0.000001, double.MaxValue, ErrorMessage = "La base no puede ser negativa.")]
        public decimal Base { get; set; }

        [XmlAttribute(AttributeName = "Impuesto")]
        public string Impuesto { get; set; }

        [XmlAttribute(AttributeName = "TipoFactor")]
        public string TipoFactor { get; set; }

        [XmlAttribute(AttributeName = "TasaOCuota")]
        [Range(0.000000, double.MaxValue, ErrorMessage = "La tasa o cuota no puede ser negativa.")]
        public decimal? TasaOCuota { get; set; }

        [XmlAttribute(AttributeName = "Importe")]
        [Range(0, double.MaxValue, ErrorMessage = "El importe no puede ser negativo.")]
        public decimal? Importe { get; set; }
    }
    [XmlRoot(ElementName = "cfdi:Retenciones", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoRetenciones
    {
        public List<NodoRetenciones> ListaTraslados;
    }
    [XmlRoot(ElementName = "Retencion")]
    public class NodoRetencion
    {
        [XmlAttribute(AttributeName = "Base")]
        [Range(0.000001, double.MaxValue, ErrorMessage = "La base no puede ser negativa.")]
        public decimal Base { get; set; }

        [XmlAttribute(AttributeName = "Impuesto")]
        public string Impuesto { get; set; }

        [XmlAttribute(AttributeName = "TipoFactor")]
        public string TipoFactor { get; set; }

        [XmlAttribute(AttributeName = "TasaOCuota")]
        [Range(0.000000, double.MaxValue, ErrorMessage = "La tasa o cuota no puede ser negativa.")]
        public decimal TasaOCuota { get; set; }

        [XmlAttribute(AttributeName = "Importe")]
        [Range(0, double.MaxValue, ErrorMessage = "El importe no puede ser negativo.")]
        public decimal Importe { get; set; }
    }

    /// <summary>
    /// Nodo condicional para capturar los impuestos aplicables al presente concepto.
    /// </summary>
    [XmlRoot(ElementName = "cfdi:Impuestos", Namespace = "http://www.sat.gob.mx/cfd/4")]
    public class NodoImpuestos
    {
        [XmlAttribute(AttributeName = "TotalImpuestosTrasladados")]
        public decimal TotalImpuestosTrasladados { get; set; }

        public NodoTraslados? NodoTraslados { get; set; }
        public NodoRetenciones? NodoRetenciones { get; set; }
    }

    [XmlRoot(ElementName = "ACuentaTerceros")]
    public class NodoACuentaTerceros
    {
        [XmlAttribute(AttributeName = "RfcACuentaTerceros")]
        public string RfcACuentaTerceros { get; set; }

        [XmlAttribute(AttributeName = "NombreACuentaTerceros")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 300 caracteres.")]
        [RegularExpression(@"[^|]{1,300}", ErrorMessage = "El nombre no puede contener el carácter '|'.")]
        public string NombreACuentaTerceros { get; set; }

        [XmlAttribute(AttributeName = "RegimenFiscalACuentaTerceros")]
        public string RegimenFiscalACuentaTerceros { get; set; }

        [XmlAttribute(AttributeName = "DomicilioFiscalACuentaTerceros")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "El código postal debe tener 5 caracteres.")]
        [RegularExpression(@"[0-9]{5}", ErrorMessage = "El código postal debe ser numérico de 5 dígitos.")]
        public string DomicilioFiscalACuentaTerceros { get; set; }
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
