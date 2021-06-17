// Matthew Rutigliano
// 4 May 2021
// Revision History: createActiveObject, createInactiveObject, queryTrue, queryFalse,
// retrieveLength, retrieveContents, blankRetrieveContents, invalidRetrieve, reset, perm shutdown written 4/30/21

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace dataMirrorTests
{
    [TestClass]
    public class dataMirrorTests
    {
        [TestMethod]
        public void createActiveObject()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data = new dataMirror(arr);
            Assert.IsTrue(data.State, "DataMirror is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveObject()
        {
            int[] badArr = null;
            dataMirror data = new dataMirror(badArr);
            Assert.IsFalse(data.State, "DataMirror is created active with an invalid parameter");
        }

        [TestMethod]
        public void queryTrue()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data = new dataMirror(arr);
            Assert.IsTrue(data.query(3), "Query cannot find value in DataMirror");
        }

        [TestMethod]
        public void queryFalse()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data = new dataMirror(arr);
            Assert.IsFalse(data.query(20), "Query finds nonexistent value in DataMirror");
        }

        [TestMethod]
        public void retrieveLength()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int retrieveLength = 3;
            dataMirror data = new dataMirror(arr);
            int[] result = data.retrieve(retrieveLength);

            Assert.AreEqual(retrieveLength, result.Length, "Retrieve array is of incorrect length");
        }

        [TestMethod]
        public void retrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            dataMirror data = new dataMirror(arr);
            int[] result = data.retrieve(10);

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
        public void blankRetrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 5, 5 };
            dataMirror data = new dataMirror(arr);
            int[] result = data.retrieve();

            bool isEqual = true;
            for (int i = 0; i < result.Length; i++)
            {
                isEqual = (result[i] == correctResult[i]);
                if (!isEqual)
                    break;
            }
            Assert.IsTrue(isEqual, "Retrieve array doesn't contain middle values");
        }

        [TestMethod]
        public void invalidRetrieve()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data = new dataMirror(arr);
            int[] result = data.retrieve(-5);
            Assert.IsFalse(data.State, "Invalid retrieve doesn't deactivate DataMirror");
        }

        [TestMethod]
        public void reset()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data = new dataMirror(arr);
            int[] result = data.retrieve(-5);
            data.reset();
            Assert.IsTrue(data.State, "Reset doesn't reactivate Dataline");
        }

        [TestMethod]
        public void permShutdown()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data = new dataMirror(arr);
            while (!data.Perm_Deactivated)
            {
                data.retrieve(-5);
                data.reset();
            }
            Assert.IsTrue(data.Perm_Deactivated, "DataMirror doesn't permenantly deactivate");
        }

        [TestMethod]
        public void permShutdownBoundsVary()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataMirror data1 = new dataMirror(arr);
            dataMirror data2 = new dataMirror(arr);
            int data1Count = 0, data2Count = 0;
            while (!data1.Perm_Deactivated | !data2.Perm_Deactivated)
            {
                if (!data1.Perm_Deactivated)
                {
                    data1.retrieve(-5);
                    data1.reset();
                    data1Count++;
                }
                if (!data2.Perm_Deactivated)
                {
                    data2.retrieve(-5);
                    data2.reset();
                    data2Count++;
                }
            }
            Assert.AreNotEqual(data1Count, data2Count, "DataMirror objects deactivate after same number of retrieves");
        }
    }
}
