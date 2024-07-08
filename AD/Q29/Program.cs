using System;
using System.Collections.Generic;
using System.Text;
public class Program
{
    static int matsize;
    static int varcounter;
    static int eqcounter;
    static bool solornot;
    static double maxval;
    static double[] sol;
    static string result;
    static double[,] save_matrix;
    static void Main()
    {
        var firstline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int N = firstline[0];
        int M = firstline[1];
        save_matrix = new double[N + 1, M + 1];
        eqcounter = N;
        varcounter = M;
        matsize = varcounter;
        sol = new double[varcounter];
        solornot = false;
        maxval = double.MinValue;
        for (int i = 0; i < N; i++)
        {
            var linemahodiat = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            for (int j = 0; j < M; j++)
            {
                save_matrix[i, j] = linemahodiat[j];
            }
        }
        var constanta = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
        for (int i = 0; i < N; i++)
        {
            save_matrix[i, M] = constanta[i];
        }
        var lezat = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
        for (int i = 0; i < M; i++)
        {
            save_matrix[N, i] = lezat[i];
        }
        string ans2 = Solve();
        Console.WriteLine(ans2);
    }
    static List<int[]> subbaz(int[] myarr)
    {
        List<int[]> subb = new List<int[]>();
        for (int i = 0; i < myarr.Length; i++)
        {
            int subsetCount = subb.Count;
            subb.Add(new int[] { myarr[i] });
            for (int j = 0; j < subsetCount; j++)
            {
                int[] newsubb = new int[subb[j].Length + 1];
                subb[j].CopyTo(newsubb, 0);
                newsubb[newsubb.Length - 1] = myarr[i];
                subb.Add(newsubb);
            }
        }
        return subb;
    }
    static string Solve()
    {
        int[] myarr = new int[eqcounter + varcounter + 1];
        for (int i = 0; i < myarr.Length ; i++)
        {
            myarr[i] = i;
        }
        var sub = subbaz(myarr);
        for (int i = 0; i < sub.Count; i++)
        {
            if (sub[i].Length == varcounter)
            {
                intersec(sub[i]);
            }
        }
        if (solornot == false)
        {
            return "No solution";
        }
        if (result != "Bounded solution")
        {
            return result;
        }
        result += '\n';
        for (int i = 0; i < varcounter; i++)
        {
            result += sol[i] + " ";
        }
        return result;
    }
    static void intersec(int[] nq)
    {
        double[,] noteq = new double[varcounter, varcounter + 1];
        for (int i = 0; i < varcounter; i++)
        {
            if (nq[i] >= eqcounter + varcounter)
            {
                for (int j = 0; j < varcounter; j++)
                {
                    noteq[i, j] = 1;
                }
                noteq[i, varcounter] = Math.Pow(10, 9);
            }
            else if (nq[i] >= eqcounter)
            {
                noteq[i, nq[i] - eqcounter] = -1;
            }
            else
                for (int j = 0; j <= varcounter; j++)
                {
                    noteq[i, j] = save_matrix[nq[i], j];
                }
        }
        RowReduce(noteq);
    }
    static void RowReduce(double[,] matrix)
    {
        for (int i = 0; i < matsize; i++)
        {
            if (matrix[i, i] == 0)
            {
                int k = i;
                while (k < matsize && matrix[k, i] == 0)
                {
                    k++;
                }
                if (k < matsize)
                {
                    for (int j = 0; j <= matsize; j++)
                    {
                        var tmp = matrix[i, j];
                        matrix[i, j] = matrix[k, j];
                        matrix[k, j] = tmp;
                    }
                }
                else
                {
                    return;
                }
            }
            if (matrix[i, i] != 1)
            {
                double tmp = matrix[i, i] ; 
                for (int j = 0; j <= matsize; j++)
                {
                    matrix[i, j] /= tmp;
                }
            }
            for (int j = 0; j < matsize; j++)
            {
                if (j != i)
                {
                    var qu = matrix[j, i];
                    for (int k = 0; k <= matsize; k++)
                    {
                        matrix[j, k] += -1 * qu * matrix[i, k];
                    }
                }
            }
        }         
        for (int i = 0; i < eqcounter; i++)
        {
            double tm = 0;
            for (int j = 0; j < varcounter; j++)
            {
               tm += save_matrix[i, j] * matrix[j, varcounter];
            }

            if (tm > save_matrix[i, varcounter]  + .001)
            {
                return;
            }           
        }
        double sum = 0;
        double val = 0;
        for (int i = 0; i < matsize; i++)
        {
            if (matrix[i, matsize] < 0 - .001 )
            {
                return;
            }
            sum += matrix[i, matsize];
            val += save_matrix[eqcounter, i] * matrix[i, matsize];
        }
        solornot = true;
        if (val > maxval)
        {
            maxval = val;
            if (sum > Math.Pow(10, 9)- .001)
            {
                result = "Infinity";
                return;
            }
            result = "Bounded solution";
            for (int i = 0; i < matsize; i++)
            {
                sol[i] = matrix[i, matsize];
            }  
        }
    }
}
