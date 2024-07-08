using System ;
using System.Collections.Generic;
 public class Program
 {
    static double[,] saver ;
    static void GaussianElimination(int num)
    {
        for(int i =0 ; i <  num ; i++)
        {
            int maxindx= i ; 
            double maxval = Math.Abs(saver[i ,i ]) ;
            for(int j = i+1 ;  j < num ; j++ )
            {
                if(maxval < Math.Abs(saver[j ,i]))
                {
                    maxval = Math.Abs(saver[j ,i]) ;
                    maxindx = j ;
                }
            }
            if(maxindx != i)
            {
                for(int k = 0 ; k <= num ; k++)
                {
                    double tmp = saver[i ,k] ; 
                    saver[i  , k]  = saver[maxindx , k] ; 
                    saver[maxindx , k] = tmp ; 
                }
            }
            for(int z = i+1 ; z < num ; z++)
            {
                double zarib = saver[z , i] / saver[i , i] ; 
                for(int k = 0 ; k <= num ; k++)
                {
                    saver[z , k] -= zarib * saver[i , k] ; 
                }
            }
        }
    }
    static void Main()
    {
        int numdish = int.Parse(Console.ReadLine()) ; 
        if(numdish == 0)
        {
            return ; 
        }
        saver = new double[numdish , numdish+1] ; 
        for(int i = 0 ; i < numdish ; i++)
        {
            var  lineinfo = Array.ConvertAll(Console.ReadLine().Split() , double.Parse) ; 
            for(int j = 0 ; j < lineinfo.Length ;  j++)
            {
                saver[i,j] = lineinfo[j]  ;
            }
        }
        GaussianElimination(numdish) ;
        int numtmp = numdish ; 
        double[] ans = new double[numdish] ; 
        for(int i = numtmp-1 ; i >= 0 ;i--)
        {
            double sum = 0 ; 
            for(int k =  i+1 ;  k  <  numtmp ; k++)
            {
                sum+= saver[i , k] * ans[k]  ;
            }
            ans[i] = (saver[i , numdish] - sum) / saver[i , i] ;
        }
        for (int i = 0; i < numdish; i++)
        {
            Console.Write($"{ans[i]:F6}");
        }
    }
 }