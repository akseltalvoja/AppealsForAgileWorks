using AppealsProject.Models;

namespace Tests
{
    public class AppealModelTests
    {
        [Fact]
        public void Appeal_SetsPropertiesCorrectly()
        {
            var appeal = new Appeal
            {
                AppealName = "Test Appeal",
                AppealDescription = "Test Description",
                DueDateTime = DateTime.Now.AddDays(7)
            };

            Assert.Equal("Test Appeal", appeal.AppealName);
            Assert.Equal("Test Description", appeal.AppealDescription);
            Assert.Equal(DateTime.Now.AddDays(7).Date, appeal.DueDateTime.Date);
            Assert.Equal(DateTime.Now.Date, appeal.AddedAt.Date);
        }

        [Fact]
        public void Appeal_AssignsIncrementalAppealId()
        {
            var initialLastAssignedId = Appeal.GetLastAssignedId();
            Appeal.ResetLastAssignedId();

            var appeal1 = new Appeal();
            var appeal2 = new Appeal();

            Assert.Equal(1, appeal1.AppealId);
            Assert.Equal(2, appeal2.AppealId);

            Appeal.SetLastAssignedId(initialLastAssignedId);
        }

    }
}