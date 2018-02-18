using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_web.Controllers;
using System.Web.Mvc;
using System.IO;
using System.Reflection;
using CRM_web.Tests.TestCases;
using CRM_web.Models;
using CRM_web.Models.Model;
using System.Threading;
using System.Web;
using Moq;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class FrmNoteControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod]
        public void NewNote()
        {
            // Arrange
            FrmNoteController controller = new FrmNoteController();

            // Act
            ViewResult result = controller.New(TestCustomer.cid) as ViewResult;

            // Assert
            NoteViewModel vm = result.Model as NoteViewModel;
            Assert.AreEqual(0, vm.Id);
            Assert.AreEqual(TestCustomer.cid, vm.Cid);
        }

        [TestMethod]
        public void EditNote()
        {
            // Arrange
            TestCase.PopulateTestData();
            FrmNoteController controller = new FrmNoteController();

            // Act
            ViewResult result = controller.Edit(TestNote.id) as ViewResult;

            // Assert
            NoteViewModel vm = result.Model as NoteViewModel;
            Assert.AreEqual(TestNote.id, vm.Id);
            Assert.AreEqual(TestNote.cid, vm.Cid);
            Assert.AreEqual(TestNote.createdBy, vm.CreatedBy);
            Assert.AreEqual(TestNote.entryDate, vm.EntryDate);
            Assert.AreEqual(TestNote.memo, vm.Memo);
            Assert.AreEqual(TestNote.category, vm.Category);
            Assert.AreEqual(TestNote.attachment, vm.Attachment);

            TestCase.CleanUp();
        }

        [TestMethod]
        public void SubmitNote()
        {
            // ---- Arrange ----
            // set up model state
            CRM_web.Models.Model.Model m = CRM_web.Models.Model.Model.GetModel();
            DefTblNotes def = new DefTblNotes();            
            m.ExecuteActionQuery(String.Format("DELETE FROM {0} WHERE {1} = {2};", def.TblName, def.Cid.Name, TestNote.cid));
            TestCase.PopulateTestData();
            m.Cid = TestNote.cid;
            m.LoadCustomers();
            m.LoadNotes(TestNote.cid);
            // set up controller and view model
            FrmNoteController controller = new FrmNoteController();
            NoteViewModel vm = new NoteViewModel();
            vm.Id = TestNote.id;
            vm.Cid = TestNote.cid;
            vm.CreatedBy = TestNote.createdBy;
            vm.EntryDate = TestNote.entryDate;
            vm.Memo = TestNote.memo;
            vm.Category = TestNote.category;
            vm.Attachment = TestNote.attachment;

            // ---- Act ----
            RedirectToRouteResult result = controller.Submit(vm) as RedirectToRouteResult;
            Thread.Sleep(1000);

            // ---- Assert ----
            Assert.AreEqual(TestNote.id, m.Notes[0][def.Id.Name]);
            Assert.AreEqual(TestNote.cid, m.Notes[0][def.Cid.Name]);
            Assert.AreEqual(TestNote.createdBy, m.Notes[0][def.CreatedBy.Name]);
            Assert.AreEqual(TestNote.entryDate, m.Notes[0][def.EntryDate.Name]);
            Assert.AreEqual(TestNote.memo, m.Notes[0][def.Memo.Name]);
            Assert.AreEqual(TestNote.category, m.Notes[0][def.Category.Name]);
            Assert.AreEqual(TestNote.attachment, m.Notes[0][def.Attachment.Name]);

            TestCase.CleanUp();
        }

        [TestMethod]
        public void UploadFile()
        {
            // Arrange
            String fileName = "Test.txt";
            NoteViewModel vm = new NoteViewModel();
            vm.Cid = TestNote.cid;
            String dstPathFile = Path.Combine(Config.GetConfig().PathCustomerFolders, vm.Cid.ToString(), fileName);
            FrmNoteController controller = new FrmNoteController();

            // mocking - style 1
            Mock<HttpPostedFileBase> mockFile = new Mock<HttpPostedFileBase>();
            mockFile.Setup(x => x.FileName).Returns(fileName);
            mockFile.Setup(x => x.ContentLength).Returns(1);
            vm.AttachmentFile = mockFile.Object;

            // mocking - style 2
            //HttpPostedFileBase mockFile = Mock.Of<HttpPostedFileBase>();
            //Mock.Get(mockFile).Setup(x => x.FileName).Returns(fileName);
            //Mock.Get(mockFile).Setup(x => x.ContentLength).Returns(1);
            //vm.AttachmentFile = mockFile;

            // Act
            MethodInfo callUploadFile = controller.GetType().GetMethod("UploadFile", BindingFlags.NonPublic | BindingFlags.Instance);
            callUploadFile.Invoke(controller, new object[] { vm });

            // Assert            
            Assert.AreEqual(fileName, vm.Attachment);
            mockFile.Verify(x => x.SaveAs(dstPathFile));  // style 1
            //Mock.Get(mockFile).Verify(x => x.SaveAs(dstPathFile)); // style 2            
        }

        [TestMethod]
        public void DownloadFile_NotFound()
        {
            // Arrange
            FrmNoteController controller = new FrmNoteController();

            // Act
            HttpNotFoundResult result = controller.DownloadFile("0", "test.txt") as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DownloadFile_Found()
        {
            // ---- Arrange ----               
            // set up source file
            String srcPath = Path.Combine(Config.GetConfig().PathCustomerFolders, TestNote.cid.ToString());
            String srcPathFile = Path.Combine(srcPath, TestNote.attachment);
            if (!Directory.Exists(srcPath))
            {
                Directory.CreateDirectory(srcPath);
            }
            if (!File.Exists(srcPathFile))
            {
                File.Create(srcPathFile).Close();                
            }
            // set up controller
            FrmNoteController controller = new FrmNoteController();

            // ---- Act ----
            FilePathResult result = controller.DownloadFile(TestNote.cid.ToString(), TestNote.attachment) as FilePathResult;

            // Assert
            Assert.IsNotNull(result);

            // clean up
            if (File.Exists(srcPathFile))
            {
                File.Delete(srcPathFile);
            }
            if (Directory.Exists(srcPath))
            {
                Directory.Delete(srcPath);
            }
        }



    }
}
