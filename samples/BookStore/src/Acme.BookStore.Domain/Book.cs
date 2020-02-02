﻿using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public ISBN ISBN { get; set; }

        protected Book()
        {

        }

        public Book(Guid id, string name, BookType type, DateTime publishDate, float price)
        :base(id)
        {
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
        }
    }
    public class ISBN : Volo.Abp.Domain.Values.ValueObject
    {
        public string EAN { get; set; }
        public string Group { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string CheckDigit { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EAN;
            yield return Group;
            yield return Publisher;
            yield return Title;
            yield return CheckDigit;
        }
    }
}