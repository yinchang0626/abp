using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.TestApp.Domain;

namespace Volo.Abp.TestApp.NHibernate.FluentMapping
{
    public class PhoneMap : ClassMap<Phone>
    {
        public PhoneMap()
        {
            Table(@"Phone");
            CompositeId().KeyProperty(x=>x.PersonId).KeyProperty(x=>x.Number);
            Map(x => x.Type);
            LazyLoad();
            
              

        }
    }

    //public class OrderMap : ClassMap<Order>
    //{
    //    public OrderMap()
    //    {
    //        Table(@"Order");
    //        LazyLoad();
    //        Id(x => x.Id)
    //          .GeneratedBy.GuidComb();
    //    }
    //}

    //public class OrderLine : Entity
    //{
    //    public virtual Guid OrderId { get; protected set; }

    //    public virtual Guid ProductId { get; protected set; }

    //    public virtual int Count { get; protected set; }

    //    protected OrderLine()
    //    {

    //    }

    //    public OrderLine(Guid orderId, Guid productId, int count)
    //    {
    //        OrderId = orderId;
    //        ProductId = productId;
    //        Count = count;
    //    }

    //    internal void ChangeCount(int newCount)
    //    {
    //        Count = newCount;
    //    }

    //    public override object[] GetKeys()
    //    {
    //        return new object[] {OrderId, ProductId};
    //    }
    //}
}