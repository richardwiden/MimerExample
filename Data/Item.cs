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
        public string Product { get; internal set; }
        public int ProducerId { get; internal set; }
        public string Producer { get; internal set; }
        public int FormatId { get; internal set; }
        public string Format { get; internal set; }
        public int CategoryId { get; internal set; }
        public string Category { get; internal set; }
    }
}

