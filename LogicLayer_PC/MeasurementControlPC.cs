﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer_PC;
using DTO_PC;

namespace LogicLayer_PC
{
	public class MeasurementControlPC
	{
		private MeasurementDataAccess measurementDataAccessObj;
		private BPMesDataGUI_DTO rawDataDTO;
		private List<double> rawDataListMC = new List<double>();


		public MeasurementControlPC()
		{
			measurementDataAccessObj = new MeasurementDataAccess();
			rawDataDTO = new BPMesDataGUI_DTO();
		}

		public List<double> GetRawData()
		{
			rawDataDTO = measurementDataAccessObj.ReadRawData();

			foreach (double rawData in rawDataDTO.RawDataList)
			{
				rawDataListMC.Add(rawData);
			}

			return rawDataListMC;
		}

	}
}
