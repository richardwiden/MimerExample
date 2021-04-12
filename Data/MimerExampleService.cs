using Microsoft.AspNetCore.Http.Features;
using Mimer.Data.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimerExample.Data
{
    public class MimerExampleService
    {
        private static string connectionString = new MimerConnectionStringBuilder() {
            Node="bebop.mimer.se", 
            Database = "test_example_db", 
            Protocol = "tcp", 
            UserId = "mimer_store", 
            Password = "SYSADM",
            AllowedTraceLevel="confidential"
        }.ToString();

        public Task<Item[]> GetItems()
        {
            return GetItems(Item.OrderBy.ItemId);
        }

        public static string GetOrderBy(Item.OrderBy orderby, Boolean asc = true)
        {
            string order = asc ? "asc" : "desc";
            switch (orderby)
            {                
                case Item.OrderBy.ItemId: return String.Format("i.item_id {0}", order);
                case Item.OrderBy.ProductId: return String.Format("p.product_id {0}", order);
                case Item.OrderBy.Product: return String.Format("p.product {0}", order);
                case Item.OrderBy.ProducerId: return String.Format("pd.producer_id {0}", order);
                case Item.OrderBy.Producer: return String.Format("pd.producer {0}", order);
                case Item.OrderBy.FormatId: return String.Format("f.format_id {0}", order);
                case Item.OrderBy.Format: return String.Format("f.format {0}", order);
                case Item.OrderBy.CategoryId: return String.Format("c.category_id {0}", order);
                case Item.OrderBy.Category: return String.Format("c.category {0}", order);
                default: throw new NotImplementedException();   
            }
        }

        public Task<Item[]> GetItems(Item.OrderBy orderBy, Boolean asc = true)
        {                        
            return Task.Run(()=> {
                var items = new List<Item>();
                
                //using var conn2 = new MimerConnection(connectionString);
                //conn2.Open();
                //var tran2 = conn2.BeginTransaction();
                //using var updateCommand = new MimerCommand("update products p set p.product='INGENTING'", conn2, tran2);
                //using var updateCommand = new MimerCommand("update items  set status='A' where product_id <> 1", conn2, tran2);
                //updateCommand.ExecuteNonQuery();
                var orderByString = GetOrderBy(orderBy, asc);

                using var conn = new MimerConnection(connectionString);
                conn.Open();
                //var tran = conn.BeginTransaction();
                using var selectCommand = new MimerCommand(
                "select i.item_id, p.product_id, p.product, pd.producer_id, pd.producer , f.format_id, f.format, c.category_id, c.category " +
                "from items i " +
                "left join products p on i.product_id = p.product_id " +
                "left join producers pd on i.producer_id = pd.producer_id " +
                "left join formats f on i.format_id = f.format_id " +
                "left join categories c on f.category_id = c.category_id " +
                "order by " + orderByString, conn
                );//,tran);                        
                using var reader = selectCommand.ExecuteReader();
                while (reader.Read()) {
                    var item = new Item() { 
                        ItemId = reader.GetInt32(0),
                        ProductId=reader.GetInt32(1),
                        Product= reader.GetString(2),
                        ProducerId = reader.IsDBNull(3) ? -1 : reader.GetInt32(3),
                        Producer = reader.IsDBNull(4) ? String.Empty : reader.GetString(4),
                        FormatId = reader.GetInt32(5),
                        Format = reader.GetString(6),
                        CategoryId = reader.GetInt32(7),
                        Category = reader.GetString(8),
                    };
                    items.Add(item);
                }
                return items.ToArray();
            });                
        }
    }
}
