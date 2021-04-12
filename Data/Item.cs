using Mimer.Data.Client;


namespace MimerExample.Data
{
    public class Item
    {
        public enum OrderBy { ItemId,ProductId,Product, ProducerId, Producer,FormatId,Format,CategoryId,Category }
        public Item() { }
        public Item(int item_id, int product_id) {
            ItemId = item_id;
            ProductId = product_id;
        }
        public int ItemId { get; set; }
        public int ProductId {get; set;}
        public string Product { get; set; }
        public int ProducerId { get; set; }
        public string Producer { get; set; }
        public int FormatId { get; set; }
        public string Format { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}

