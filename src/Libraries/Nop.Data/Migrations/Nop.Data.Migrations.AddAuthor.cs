using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations
{
    [NopMigration("2021/11/13 19:40:28:2551770", "Product. Add Author")]
    public class AddAuthor : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column(nameof(Product.Author))
            .OnTable(nameof(Product))
            .AsString(255)
            .Nullable();
        }
    }
}
