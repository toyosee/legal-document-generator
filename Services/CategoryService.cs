using System.Collections.Generic;

namespace CourtDocumentGenerator.Services
{
    public class CategoryService
    {
        private readonly List<string> lostItems = new()
        {
            "Wallet",
            "A.T.M Card",
            "Phone",
            "Birth Certificate",
            "Marriage Certificate",
        };

        public List<string> GetLostItems()
        {
            return lostItems;
        }
    }
}
