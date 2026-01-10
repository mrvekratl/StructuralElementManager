using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.UI.Models
{
    public class ElementTreeItemModel
    {
        public StructuralElement Element { get; set; }

        public string Icon
        {
            get
            {
                return Element switch
                {
                    StructuralColumn => "📐",
                    StructuralBeam => "═══",
                    StructuralSlab => "▭",
                    _ => "■"
                };
            }
        }

        public string DisplayName => $"{Icon} {Element.Name}";
        public string TypeName => Element.GetElementType();

        public ElementTreeItemModel(StructuralElement element)
        {
            Element = element;
        }
    }
}
