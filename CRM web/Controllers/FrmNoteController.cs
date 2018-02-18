using CRM_web.Models;
using CRM_web.Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM_web.Controllers
{
    [Authorize]
    public class FrmNoteController : Controller
    {
        Model model = Model.GetModel();
        DefTblNotes tblDef = Model.GetModel().TblNotes;


        // GET: FrmNote
        public ActionResult New(int cid)
        {
            NoteViewModel vm = new NoteViewModel();
            vm.Id = 0;
            vm.Cid = cid;
                                   
            return View("FrmNote", vm);
        }

        public ActionResult Edit(int id)
        {
            NoteViewModel vm = new NoteViewModel();
            vm.Id = id;

            try
            {
                DataRow dr = (from d in model.GetNote(id).Table.AsEnumerable()                              
                              select d).First();
                
                vm.Cid = int.Parse(dr[tblDef.Cid.Name].ToString());
                vm.CreatedBy = dr[tblDef.CreatedBy.Name].ToString();
                vm.EntryDate = dr[tblDef.EntryDate.Name] as DateTime?;
                vm.Memo = dr[tblDef.Memo.Name].ToString();
                vm.Category = dr[tblDef.Category.Name].ToString();
                vm.Attachment = dr[tblDef.Attachment.Name].ToString();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("noData", e);
            }

            return View("FrmNote", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(NoteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("FrmNote", vm);
            }

            vm.EntryDate = DateTime.Now;
            vm.CreatedBy = "anonymous";

            UploadFile(vm);

            EntryMode em = vm.Id == 0 ? EntryMode.New : EntryMode.Edit;
            model.Submit(vm.getMap(), model.TblNotes.TblName, new Tuple<ColDef, object>(model.TblNotes.Id, vm.Id), em);

            return RedirectToAction("Search", "Cockpit", new { cid = vm.Cid.ToString() });
        }

        private void UploadFile(NoteViewModel vm)
        {
            if (vm.AttachmentFile != null && vm.AttachmentFile.ContentLength > 0)
            {
                vm.Attachment = Path.GetFileName(vm.AttachmentFile.FileName);
                String dstPath = Path.Combine(Config.GetConfig().PathCustomerFolders, vm.Cid.ToString());
                String dstPathFile = Path.Combine(dstPath, vm.Attachment);
                vm.AttachmentFile.SaveAs(dstPathFile);
            }            
        }       

        public ActionResult OpenAttachment(int id)
        {
            try
            {            
                DataRow dr = (from d in model.GetNote(id).Table.AsEnumerable()
                              select d).First();

                String cid = dr[tblDef.Cid.Name].ToString();
                String file = dr[tblDef.Attachment.Name].ToString();

                return DownloadFile(cid, file);
            }
            catch(Exception e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult DownloadFile(String cid, String file)
        {
            String pathFile = "";
            if (cid == null || file == null)
            {
                return HttpNotFound();
            }
            else
            {
                pathFile = Path.Combine(Config.GetConfig().PathCustomerFolders, cid, file);
            }
            
            if (System.IO.File.Exists(pathFile))
            {
                return File(pathFile, "*/*");
            }
            else
            {
                return HttpNotFound();
            }                       
        }
    }
}