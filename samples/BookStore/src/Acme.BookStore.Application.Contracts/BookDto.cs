using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        public ISBNDto ISBN { get; set; }
    }
    public class ISBNDto
    {
        public string EAN { get; set; }
        public string Group { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string CheckDigit { get; set; }
    }
}