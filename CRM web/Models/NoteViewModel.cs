using CRM_web.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM_web.Models
{
    public class NoteViewModel
    {        
        [Required]
        public int Id { get; set; }

        [Required]
        public int Cid { get; set; }

        public String CreatedBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public String Memo { get; set; }

        public String Category { get; set; }

        public String Attachment { get; set; }

        public HttpPostedFileBase AttachmentFile { get; set; }        

        public Dictionary<ColDef, object> getMap()
        {
            DefTblNotes tblDef = Model.Model.GetModel().TblNotes;
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();

            map.Add(tblDef.Id, Id);
            map.Add(tblDef.Cid, Cid);
            map.Add(tblDef.CreatedBy, CreatedBy);
            map.Add(tblDef.EntryDate, EntryDate);
            map.Add(tblDef.Memo, Memo);
            map.Add(tblDef.Category, Category);
            map.Add(tblDef.Attachment, Attachment);

            return map;
        }
    }
}