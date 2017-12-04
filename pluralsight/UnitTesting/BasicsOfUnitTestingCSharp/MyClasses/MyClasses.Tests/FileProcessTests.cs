using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClasses.Tests
{
    [TestClass]
    public class FileProcessTests
    {
        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(@"C:\Windows\Regedit.exe");

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(@"C:\Windows\BadFileName.bad");

            Assert.IsFalse(fromCall);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArguementNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }
    }
}
