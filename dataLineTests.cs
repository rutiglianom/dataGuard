// Matthew Rutigliano
// 4 May 2021
// Revision History: createActiveObject, createInactiveObject, queryTrue, queryFalse,
// retrieveLength, retrieveContents, invalidRetrieve written 4/28/21
// reset written 4/30/21

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace dataLineTests
{
    [TestClass]
    public class dataLineTests
    {
        [TestMethod]
        public void createActiveObject()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataLine data = new dataLine(arr);
            Assert.IsTrue(data.State, "Dataline is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveObject()
        {
            int[] badArr = null;
            dataLine data = new dataLine(badArr);
            Assert.IsFalse(data.State, "Dataline is created active with an invalid parameter");
        }

        [TestMethod]
        public void queryTrue()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataLine data = new dataLine(arr);
            Assert.IsTrue(data.query(3), "Query cannot find value in Dataline");
        }

        [TestMethod]
        public void queryFalse()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataLine data = new dataLine(arr);
            Assert.IsFalse(data.query(20), "Query finds nonexistent value in Dataline");
        }

        [TestMethod]
        public void retrieveLength()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int retrieveLength = 3;
            dataLine data = new dataLine(arr);
            int[] result = data.retrieve(retrieveLength);

            Assert.AreEqual(retrieveLength, result.Length, "Retrieve array is of incorrect length");
        }

        [TestMethod]
        public void retrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 1, 2, 3 };
            dataLine data = new dataLine(arr);
            int[] result = data.retrieve(3);

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Retrieve array contains incorrect values");
        }

        [TestMethod]
        public void invalidRetrieve()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataLine data = new dataLine(arr);
            int[] result = data.retrieve(-5);
            Assert.IsFalse(data.State, "Invalid retrieve doesn't deactivate Dataline");
        }

        [TestMethod]
        public void reset()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataLine data = new dataLine(arr);
            int[] result = data.retrieve(-5);
            data.reset();
            Assert.IsTrue(data.State, "Reset doesn't reactivate Dataline");
        }
    }
}
