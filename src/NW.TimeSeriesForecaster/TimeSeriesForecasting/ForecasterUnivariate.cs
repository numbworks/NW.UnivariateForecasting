using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NW.TimeSeriesForecaster
{
    public class ForecasterUnivariate : IForecaster
    {

        // Fields
        // Properties
        public IExceptionMessage ExceptionMessage { get; set; } = new ExceptionMessage();
        public IForecastingStrategiesUnivariate ForecastingStrategiesUnivariate { get; set; } = new ForecastingStrategiesUnivariate();
        public IRoundingStategies RoundingStrategies { get; set; } = new RoundingStategies();

        // Constructors
        public ForecasterUnivariate() { }

        // Methods
        public ArrayList GetForecastedObservations(SlidingWindow objSlidingWindow)
        {

            /* 
             * 
             * In a successful scenario, at the beginning of this data transformation pipeline 
             * we have a Json that looks like this:
             *             
             * ...
             *	"TimeSeriesCollection": [
             *					{
             *						"ObservationName": "mc-7ab447dc-cbb5-4c27-a993-032e4291d199",
             *						"TimeSeriesId": 1, 
             *						"X_Actual": 635.69, 
             *						"Y1_Forecasted": 612.27 
             *						"TagCollection": "[ \"SubscriptionName=Sitecore Cloud - Western and Southern Financial Group - c1aafed5\", \"Currency=EUR\" ]"
             *					},
             *					{
             *						"ObservationName": "mc-7ab447dc-cbb5-4c27-a993-032e4291d199",
             *						"TimeSeriesId": 2, 
             *						"X_Actual": 612.27, 
             *						"Y1_Forecasted": 632.94
             *						"TagCollection": "[ \"SubscriptionName=Sitecore Cloud - Western and Southern Financial Group - c1aafed5\", \"Currency=EUR\" ]"
             *					},
             *					...
             *					{
             *						"ObservationName": "mc-7ab447dc-cbb5-4c27-a993-032e4291d199",
             *						"TimeSeriesId": 7, 
             *						"X_Actual": 568.34, 
             *						"Y1_Forecasted": null
             *						"TagCollection": "[ \"SubscriptionName=Sitecore Cloud - Western and Southern Financial Group - c1aafed5\", \"Currency=EUR\" ]"
             *					}
             *				],
             *	...
             *	
             *	At the end, a List<ForecastedObservationUnivariate>, in which each object looks like this:
             *	
             *	    C: 			        3.67
             *	    E: 			        -2.63
             *	    ObservationName: 	"mc-7ab447dc-cbb5-4c27-a993-032e4291d199"
             *	    SlidingWindowId: 	"SWID20180501145357917"
             *	    TagCollection: 		"[ \"SubscriptionName=Sitecore Cloud - Lakrids by Johan Bülow\", \"Currency=EUR\" ]"
             *	    X_Actual: 		    35.66
             *	    Y1_Forecasted: 		128.11	
             *	
             *	The RoundingStrategy is TwoDecimalDigits, because X_Actual values use the same.
             *	
             */

            string msgSuccess = "A List<ForecastedObservationUnivariate> has been successfully obtained out of the provided SlidingWindowJson.";
            string errFail = "It hasn't been possible to obtain a List<ForecastedObservationUnivariate> out of the provided SlidingWindowJson.";

            try
            {

                ArrayList arrlReturn = ValidateParameters(objSlidingWindow);
                if (ArrayListReturn.IsFail(arrlReturn))
                    return ArrayListReturn.AppendMessages(arrlReturn, new List<string>() { errFail });

                List<string> listObservationNames = new HashSet<string>(
                    objSlidingWindow.TimeSeriesCollection.Select(Item => Item.ObservationName))
                    .ToList<string>();

                List<ForecastedObservationUnivariate> listForecasted = new List<ForecastedObservationUnivariate>();
                for (int i = 0; i < listObservationNames.Count; i++)
                {

                    List<SlidingWindowTimeSeries> listCurrentCollection = new List<SlidingWindowTimeSeries>();
                    listCurrentCollection.AddRange(objSlidingWindow.TimeSeriesCollection);
                    listCurrentCollection.RemoveAll(Item => Item.ObservationName != listObservationNames[i]);

                    // The TagCollection is the same for a List<*TimeSeries> belonging to the same observation
                    string strTagCollection =
                        (listCurrentCollection
                        .Where(Item => Item.ObservationName == listObservationNames[i])
                        .First()).TagCollection;

                    arrlReturn = GetForecastedObservation(
                            listObservationNames[i],
                            objSlidingWindow.SlidingWindowId,
                            listCurrentCollection,
                            strTagCollection);
                    if (ArrayListReturn.IsFail(arrlReturn))
                        return ArrayListReturn.AppendMessages(arrlReturn, new List<string>() { errFail });

                    listForecasted.Add(
                        (ForecastedObservationUnivariate)arrlReturn[2]);

                };

                return ArrayListReturn.CreateSuccess(msgSuccess, listForecasted);

            }
            catch (Exception e)
            {

                return ArrayListReturn.CreateFail(ExceptionMessage.Format(e));

            }

        }
        private ArrayList GetForecastedObservation
            (string strObservationName,
             string strSlidingWindowId,
             List<SlidingWindowTimeSeries> listTimeSeries,
             string strTagCollection)
        {

            string msgSuccess =
                "A ForecastedObservationUnivariate object has been successfully obtained out of the provided List<SlidingWindowTimeSeries> object.";
            string errFail = "It hasn't been possible to obtain a ForecastedObservationUnivariate object out of the provided List<SlidingWindowTimeSeries> object.";

            try
            {

                ForecastedObservationUnivariate objForecasted = new ForecastedObservationUnivariate();
                objForecasted.ObservationName = strObservationName;
                objForecasted.SlidingWindowId = strSlidingWindowId;
                objForecasted.TagCollection = strTagCollection;

                ForecastingStrategiesUnivariate.CalculateValues
                    (listTimeSeries, ref objForecasted, RoundingStrategies.GetTwoDecimalDigitStrategy());

                return ArrayListReturn.CreateSuccess(msgSuccess, objForecasted);

            }
            catch (Exception e)
            {

                return ArrayListReturn.CreateFail(new List<string>() {
                        ExceptionMessage.Format(e),
                        errFail
                });

            }

        }
        private ArrayList ValidateParameters(SlidingWindow objSlidingWindow)
        {

            string msgSuccess = "All the provided parameters are valid.";
            string errTemplate = "{0} is empty or null.";

            if (objSlidingWindow == null)
                return ArrayListReturn.CreateFail(
                    String.Format(errTemplate, "The provided SlidingWindow object"));

            // Checks if the provided Sliding Window object is a successful one
            if (!objSlidingWindow.IsSuccess)
                return ArrayListReturn.CreateFail(objSlidingWindow.ErrorMessage);

            if (String.IsNullOrEmpty(objSlidingWindow.SlidingWindowId))
                return ArrayListReturn.CreateFail(
                    String.Format(errTemplate, "The SlidingWindowId of the provided SlidingWindow object"));

            if (objSlidingWindow.TimeSeriesCollection == null)
                return ArrayListReturn.CreateFail(
                    String.Format(errTemplate, "The TimeSeriesCollection of the provided SlidingWindow object"));

            if (objSlidingWindow.TimeSeriesCollection.Count == 0)
                return ArrayListReturn.CreateFail(
                    String.Format(errTemplate, "The TimeSeriesCollection of the provided SlidingWindow object"));

            int intNullObservationNames =
                objSlidingWindow.TimeSeriesCollection
                .Where(Item => String.IsNullOrEmpty(Item.ObservationName))
                .Select(Item => Item.ObservationName)
                .ToList<string>().Count;
            if (intNullObservationNames > 0)
                return ArrayListReturn.CreateFail(
                    String.Format(errTemplate, "The TimeSeriesCollection of the provided SlidingWindow object contains at least one ObservationName that"));

            return ArrayListReturn.CreateSuccess(msgSuccess, null);

        }

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 25.04.2018

*/