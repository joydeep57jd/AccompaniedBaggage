using System.Xml.Linq;

namespace AccompaniedBaggage.Model.Request
{
    public class XmlConvertercs
    {
        public static string ConvertToXmlCreditNoteDetail(List<CreditNoteDetail> details)
        {
            var xml = new XElement("CreditNoteDetails",
                details.Select(d =>
                    new XElement("Detail",
                        new XElement("CreditNoteDetailId", d.CreditNoteDetailId),
                        new XElement("ChargesTypeId", d.ChargesTypeId),
                        new XElement("CreditNoteId", d.CreditNoteId),
                        new XElement("ChargeType", d.ChargeType ?? string.Empty),
                        new XElement("ChargeName", d.ChargeName ?? string.Empty),
                        new XElement("SACCode", d.SACCode ?? string.Empty),
                        new XElement("Quantity", d.Quantity ?? 0),
                        new XElement("Rate", d.Rate ?? 0),
                        new XElement("Inv_Amount", d.Inv_Amount ?? 0),
                        new XElement("Taxable", d.Taxable ?? 0),
                        new XElement("IGSTPer", d.IGSTPer ?? 0),
                        new XElement("IGSTAmt", d.IGSTAmt ?? 0),
                        new XElement("CGSTPer", d.CGSTPer ?? 0),
                        new XElement("CGSTAmt", d.CGSTAmt ?? 0),
                        new XElement("SGSTPer", d.SGSTPer ?? 0),
                        new XElement("SGSTAmt", d.SGSTAmt ?? 0),
                        new XElement("Total", d.Total ?? 0)
                    )
                )
            );

            return xml.ToString();
        }
    }
}
