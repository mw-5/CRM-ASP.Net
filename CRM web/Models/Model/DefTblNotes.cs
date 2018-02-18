using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_web.Models.Model
{
    public class DefTblNotes
    {
        public DefTblNotes()
        {
            TblName = "notes";
            Id = new ColDef() { Name = "id", Type = ColTypes.Numeric };
            Cid = new ColDef() { Name = "cid", Type = ColTypes.Numeric };
            CreatedBy = new ColDef() { Name = "created_by", Type = ColTypes.Text };
            EntryDate = new ColDef() { Name = "entry_date", Type = ColTypes.Date };
            Memo = new ColDef() { Name = "memo", Type = ColTypes.Text };
            Category = new ColDef() { Name = "category", Type = ColTypes.Text };
            Attachment = new ColDef() { Name = "attachment", Type = ColTypes.Text };
        }

        public String TblName { get; private set; }
        public ColDef Id { get; private set; }
        public ColDef Cid { get; private set; }
        public ColDef CreatedBy { get; private set; }
        public ColDef EntryDate { get; private set; }
        public ColDef Memo { get; private set; }
        public ColDef Category { get; private set; }
        public ColDef Attachment { get; private set; }
    }
}
