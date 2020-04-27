using System;
using System.Collections;
using System.Web.Script.Serialization;

namespace NW.TimeSeriesForecaster
{
    public class SlidingWindowJsonDeserializer : ISlidingWindowJsonDeserializer
    {

        // Fields
        // Properties
        public IExceptionMessage ExceptionMessage { get; set; } = new ExceptionMessage();

        // Constructors
        public SlidingWindowJsonDeserializer() { }

        // Methods
        public ArrayList Do(string strJson)
        {

            /*
             *  Success:
             *  
             *  {
             *      "ObservationDescription": "Managed Cloud, ExtendedCost",
             *      "StepsAhead": 1,
             *      "Duration": 6,
             *      "IsSuccess": true,
             *      "ErrorMessage": null,
             *      "SlidingWindowId": "SWID<datetime>",
             *      "TimeSeriesCollection": [
             *                                    {
             *                                         "ObservationName": "mc-7ab447dc-cbb5-4c27-a993-032e4291d199",
             *                                         "TimeSeriesId": 1, 
             *                                         "X_Actual": 635.69, 
             *                                         "Y1_Forecasted": 612.27 
             *                                         "TagCollection": "[ \"SubscriptionName=Sitecore Cloud - Western and Southern Financial Group - c1aafed5\", \"Currency=EUR\" ]"
             *                                         },
             *                                    {
             *                                         "ObservationName": "mc-7ab447dc-cbb5-4c27-a993-032e4291d199",
             *                                         "TimeSeriesId": 2, 
             *                                         "X_Actual": 612.27, 
             *                                         "Y1_Forecasted": 632.94
             *                                         "TagCollection": "[ \"SubscriptionName=Sitecore Cloud - Western and Southern Financial Group - c1aafed5\", \"Currency=EUR\" ]"
             *                                     },
             *                                     ...
             *                                     {
             *                                         "ObservationName": "mc-7ab447dc-cbb5-4c27-a993-032e4291d199",
             *                                         "TimeSeriesId": 7, 
             *                                         "X_Actual": 568.34, 
             *                                         "Y1_Forecasted": null
             *                                         "TagCollection": "[ \"SubscriptionName=Sitecore Cloud - Western and Southern Financial Group - c1aafed5\", \"Currency=EUR\" ]"
             *                                     }
             *                               ],
             *      "StartDate": "2017-09-01",
             *      "EndDate": "2018-02-28",
             *      "TargetDate": "2018-03-01",
             *      "DurationUnit": "months"
             * }
             *
             * Error:
             *
             * {
             *      "ObservationDescription": "Managed Cloud, ExtendedCost",
             *      "StepsAhead": 1,
             *      "Duration": 6,
             *      "IsSuccess": false,
             *      "ErrorMessage": "<explanation of what happened>",
             *      "SlidingWindowId": "SWID<datetime>",
             *      "TimeSeriesCollection": null,
             *      "StartDate": null,
             *      "EndDate": null,
             *      "TargetDate": null,
             *      "DurationUnit": "months"
             * }
             * 
             */

            string msgSuccess = "The provided Sliding Window JSON has been successfully deserialized.";
            string errFail = "It hasn't been possible to deserialize the provided Sliding Window JSON ('{0}').";

            try
            {

                JavaScriptSerializer objSerializer = new JavaScriptSerializer();
                SlidingWindow objSlidingWindow = objSerializer.Deserialize<SlidingWindow>(strJson);

                if (!IsValid(objSlidingWindow))
                    return ArrayListReturn.CreateFail(
                                String.Format(errFail, strJson));

                return ArrayListReturn.CreateSuccess(msgSuccess, objSlidingWindow);

            }
            catch (Exception e)
            {

                return ArrayListReturn.CreateFail(ExceptionMessage.Format(e));

            }

        }
        private bool IsValid(SlidingWindow objSlidingWindow)
        {

            if (objSlidingWindow == null) return false;

            // JavaScriptSerializer seems to serialize wrong JSONs anyway, and return the following object:
            if (objSlidingWindow.ObservationDescription == null
                && objSlidingWindow.StepsAhead == 0
                && objSlidingWindow.Duration == 0
                && objSlidingWindow.IsSuccess == false
                && objSlidingWindow.ErrorMessage == null
                && objSlidingWindow.SlidingWindowId == null
                && objSlidingWindow.TimeSeriesCollection == null
                && objSlidingWindow.StartDate == null
                && objSlidingWindow.EndDate == null
                && objSlidingWindow.TargetDate == null
                && objSlidingWindow.DurationUnit == null)
                return false;

            return true;

        }

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 09.05.2018

*/