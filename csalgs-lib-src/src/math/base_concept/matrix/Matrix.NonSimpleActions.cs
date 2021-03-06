﻿using System;

// ReSharper disable CheckNamespace
namespace csalgs.math
// ReSharper restore CheckNamespace
{
    public partial class Matrix
    {
        /// <summary>
        /// Transose matrix
        /// </summary>
        /// <returns>new matrix result of transosing current matrix</returns>
        public Matrix Transpose()
        {
            var result = GetMatrix(ColumnsCount, RowsCount);

            for (int i = 0; i < result.RowsCount; i++)
            {
                for (int j = 0; j < result.ColumnsCount; j++)
                {
                    result[i, j] = this[j, i];
                }
            }

            return result;
        }

        /// <summary>
        /// Get minor matrix
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns>minor matrix</returns>
        public Matrix GetMinor(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex > RowsCount - 1 || columnIndex < 0 || columnIndex > ColumnsCount - 1) throw new ArgumentOutOfRangeException("columnIndex");


            var resultMatrix = GetMatrix(RowsCount - 1, ColumnsCount - 1);
	        for (var i = 0; i < RowsCount; i++)
	        {
		        for (var j = 0; j < ColumnsCount; j++)
                {
	                if (i == rowIndex || j == columnIndex) continue;
	                var newi = i;
	                var newj = j;
	                if (i > rowIndex)
	                {
		                newi--;
	                }
	                if (j > columnIndex)
	                {
		                newj--;
	                }
	                resultMatrix[newi, newj] = this[i, j];
                }
	        }
	        return resultMatrix;
        }

        /// <summary>
        /// Get union matrix
        /// </summary>
        /// <returns>union matrix</returns>
        public Matrix GetUnion()
        {
            if (RowsCount != ColumnsCount) throw new ArgumentException("current matrix not square");

            var result = GetQuadroMatrix(RowsCount);
            var transposeM = Clone().Transpose();

            for (int i = 0; i < ColumnsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    result[i, j] = Math.Pow(-1, i + j) * transposeM.GetMinor(i, j).RecursiveDetirminant();
                }

            }


            return result;
        }

        /// <summary>
        /// Get inverse matrix
        /// </summary>
        /// <returns>inverse matrix</returns>
        public Matrix GetInverse()
        {
            if (RowsCount != ColumnsCount) throw new InvalidOperationException("not square");

            Matrix result = GetUnion();
            double det = RecursiveDetirminant();
            return result / det;
        }

        /// <summary>
        /// Detirminant. Recursive method(too slow for size>10)
        /// </summary>
        /// <returns></returns>
        public double RecursiveDetirminant()
        {
            if (ColumnsCount != RowsCount) throw new InvalidOperationException("Matrix is not squared");

            double result = 0;

            if (ColumnsCount == 2 && RowsCount == 2)
            {
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            }
	        
			for (int i = 0; i < ColumnsCount; i++)
	        {

		        result += this[0, i] * (Math.Pow(-1, i)) * GetMinor(0, i).RecursiveDetirminant();
	        }

	        return result;
        }

        
    }
}
