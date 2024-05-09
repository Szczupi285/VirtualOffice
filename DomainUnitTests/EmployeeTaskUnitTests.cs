using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.EmployeeTask;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace DomainUnitTests
{
    public class EmployeeTaskUnitTests
    {
        #region EmployeeTaskId

        [Fact]
        public void EmptyEmployeeTaskId_ShouldReturnEmptyEmployeeTaskIdException()
        {
            Assert.Throws<EmptyEmployeeTaskIdException>(()
                => new EmployeeTaskId(Guid.Empty));
        }
        [Fact]
        public void ValidEmployeeTaskId_ValidGuidToAppUserIdConversion_ShouldEqual()
        {
            var guid = Guid.NewGuid();

            EmployeeTaskId id = guid;

            Assert.Equal(id.Value, guid);
        }
        [Fact]
        public void ValidEmployeeTaskId_ValidAppUserIdToGuidConversionShouldEqual()
        {

            EmployeeTaskId id = new EmployeeTaskId(Guid.NewGuid());

            Guid guid = id;

            Assert.Equal(id.Value, guid);
        }
        #endregion

        #region EmployeeTaskTitle

        [Fact]
        public void EmptyEmployeeTaskName_ShouldReturnEmptyEmployeeTaskNameException()
        {
            Assert.Throws<EmptyEmployeeTaskTitleException>(()
                => new EmployeeTaskTitle(""));
        }

        [Fact]
        public void NullEmployeeTaskName_ShouldReturnEmptyEmployeeTaskNameException()
        {
            Assert.Throws<EmptyEmployeeTaskTitleException>(()
                => new EmployeeTaskTitle(null));
        }

        [Fact]
        public void ValidEmployeeTaskName_ValidStringToEmployeeTaskTitleConversion_ShouldEqual()
        {
            string title = "ExampleTitle";

            EmployeeTaskTitle tit = title;

            Assert.Equal(title, tit);
        }
        [Fact]
        public void ValidEmployeeTaskTitle_ValidEmployeeTaskTitleToStringConversionShouldEqual()
        {
            EmployeeTaskTitle tit = new EmployeeTaskTitle("ExampleTitle");

            string title = tit;

            Assert.Equal(tit, title);

        }
        [Fact]
        public void InvalidEmployeeTaskTitle_TooLongTitle()
        {
            string s = new string('a', 101);
            Assert.Throws<TooLongEmployeeTaskTitleException>(() => new EmployeeTaskTitle(s));
        }
        [Fact]
        public void ValidEmployeeTaskTitle_HundredChars()
        {
            string s = new string('a', 100);
            new EmployeeTaskTitle(s);
        }
        [Fact]
        public void ValidEmployeeTaskTitle_OneChar()
        {
            new EmployeeTaskTitle("a");
        }


        #endregion
    }
}
