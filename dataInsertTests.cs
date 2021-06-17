// Matthew Rutigliano
// 4 May 2021
// Revision History: createActiveObject, createInactiveObject, queryTrue, queryFalse,
// retrieveLength, retrieveContents, invalidRetrieve, reset, insertedValueVariesBtwObjects,
// insertedValueChanges, insertedValueChangeQueriesVary written 5/1/21

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace dataInsertTests
{
    [TestClass]
    public class dataInsertTests
    {
        [TestMethod]
        public void createActiveObject()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataInsert data = new dataInsert(arr);
            Assert.IsTrue(data.State, "DataInsert is created inactive with a valid parameter");
        }

        [TestMethod]
        public void createInactiveObject()
        {
            int[] badArr = null;
            dataInsert data = new dataInsert(badArr);
            Assert.IsFalse(data.State, "DataInsert is created active with an invalid parameter");
        }

        [TestMethod]
        public void queryTrue()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataInsert data = new dataInsert(arr);
            Assert.IsTrue(data.query(3), "Query cannot find value in DataInsert");
        }

        [TestMethod]
        public void queryFalse()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataInsert data = new dataInsert(arr);
            Assert.IsFalse(data.query(20), "Query finds nonexistent value in DataInsert");
        }

        [TestMethod]
        public void retrieveLength()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int retrieveLength = 3;
            dataInsert data = new dataInsert(arr);
            int[] result = data.retrieve(retrieveLength);

            Assert.AreEqual(retrieveLength, result.Length, "Retrieve array is of incorrect length");
        }

        [TestMethod]
        public void retrieveContents()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            int[] correctResult = new int[] { 1, 2, 3 };
            dataInsert data = new dataInsert(arr);
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
            dataInsert data = new dataInsert(arr);
            int[] result = data.retrieve(-5);
            Assert.IsFalse(data.State, "Invalid retrieve doesn't deactivate DataInsert");
        }

        [TestMethod]
        public void reset()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            dataInsert data = new dataInsert(arr);
            int[] result = data.retrieve(-5);
            data.reset();
            Assert.IsTrue(data.State, "Reset doesn't reactivate DataInsert");
        }

        [TestMethod]
        public void insertedValueVariesBtwObjects()
        {
            int[] arr = new int[] { 1, 2, 3 };
            int retrieveLength = arr.Length + 1;
            dataInsert data1 = new dataInsert(arr);
            int[] result1 = data1.retrieve(retrieveLength);
            dataInsert data2 = new dataInsert(arr);
            int[] result2 = data2.retrieve(retrieveLength);

            Assert.AreNotEqual(result1[retrieveLength - 1], result2[retrieveLength - 1], "Inserted value doesn't vary between objects");
        }

        [TestMethod]
        public void insertedValueChanges()
        {
            int TEST_LENGTH = 100;
            int[] arr = new int[] { 1, 2, 3 };
            int retrieveLength = arr.Length + 1;
            dataInsert data = new dataInsert(arr);
            int[] result = data.retrieve(retrieveLength);
            int originalVal = result[retrieveLength - 1];
            int i = 0;
            while ((result[retrieveLength - 1] == originalVal) & (i < TEST_LENGTH))
            {
                data.query(1);
                result = data.retrieve(retrieveLength);
                i++;
            }

            Assert.AreNotEqual(originalVal, result[retrieveLength - 1], "Inserted value doesn't change after some amount of queries");
        }

        [TestMethod]
        public void insertedValueChangeQueriesVary()
        {
            int TEST_LENGTH = 100;
            int[] arr = new int[] { 1, 2, 3 };
            int retrieveLength = arr.Length + 1;

            dataInsert data1 = new dataInsert(arr);
            int[] result1 = data1.retrieve(retrieveLength);
            int originalVal1 = result1[retrieveLength - 1];
            int i = 0;
            while ((result1[retrieveLength - 1] == originalVal1--) & (i < TEST_LENGTH))
            {
                data1.query(1);
                result1 = data1.retrieve(retrieveLength);
                i++;
            }

            dataInsert data2 = new dataInsert(arr);
            int[] result2 = data2.retrieve(retrieveLength);
            int originalVal2 = result2[retrieveLength - 1];
            int j = 0;
            while ((result2[retrieveLength - 1] == originalVal2--) & (j < TEST_LENGTH))
            {
                data2.query(1);
                result2 = data2.retrieve(retrieveLength);
                j++;
            }

            Assert.AreNotEqual(i, j, "Different objects change inserted value after same amount of queries");
        }
    }
}
