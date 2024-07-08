using System;
using System.Collections.Generic;


public class Program
{
    static int[] computecharclass(string txt, int[] order)
    {
        int[] cls = new int[txt.Length];
        int tmp = 0;
        cls[order[0]] = tmp;
        for (int i = 1; i < txt.Length; i++)
        {
            if (txt[order[i]] == txt[order[i - 1]])
            {
                cls[order[i]] = tmp;
            }
            else
            {
                tmp++;
                cls[order[i]] = tmp;
            }
        }
        return cls;
    }
    static int[] sortt(string txt)
    {
        int[] order = new int[txt.Length];
        int[] count = new int[5];
        for (int i = 0; i < txt.Length; i++)
        {
            switch (txt[i])
            {
                case '$':
                    count[0]++;
                    break;
                case 'A':
                    count[1]++;
                    break;
                case 'C':
                    count[2]++;
                    break;
                case 'G':
                    count[3]++;
                    break;
                case 'T':
                    count[4]++;
                    break;
            }
        }
        for (int i = 1; i < count.Length; i++)
        {
            count[i] = count[i] + count[i - 1];
        }
        for (int i = txt.Length - 1; i >= 0; i--)
        {
            switch (txt[i])
            {
                case '$':
                    count[0]--;
                    order[count[0]] = i;
                    break;
                case 'A':
                    count[1]--;
                    order[count[1]] = i;
                    break;
                case 'C':
                    count[2]--;
                    order[count[2]] = i;
                    break;
                case 'G':
                    count[3]--;
                    order[count[3]] = i;
                    break;
                case 'T':
                    count[4]--;
                    order[count[4]] = i;
                    break;
            }
        }
        return order;
    }
    static int[] sortdouble(string txt, int L, int[] order, int[] cls)
    {
        int[] count = new int[txt.Length];
        int[] neworder = new int[txt.Length];
        for (int i = 0; i < txt.Length; i++)
        {
            count[cls[i]]++;
        }
        for (int i = 1; i < txt.Length; i++)
        {
            count[i] = count[i] + count[i - 1];
        }
        for (int i = txt.Length - 1; i >= 0; i--)
        {
            int start = (order[i] - L + txt.Length) % txt.Length;
            int cl = cls[start];
            count[cl] = count[cl] - 1;
            neworder[count[cl]] = start;
        }
        return neworder;
    }
    static int[] updatecls(int[] order, int[] cls, int L)
    {
        int[] newcls = new int[order.Length];
        int tmp = 0;
        newcls[order[0]] = tmp;
        for (int i = 1; i < order.Length; i++)
        {
            int cur = order[i];
            int prev = order[i - 1];
            int mid = (order[i] + L) % order.Length;
            int prevmid = (order[i - 1] + L) % order.Length;
            if (cls[cur] != cls[prev] || cls[mid] != cls[prevmid])
            {
                tmp++;
                newcls[order[i]] = tmp;
            }
            else
            {
                newcls[order[i]] = tmp;

            }
        }
        return newcls;
    }
    static int[] BuildSuffixArray(string txt)
    {
        int[] order = sortt(txt);
        int[] cls = computecharclass(txt, order);
        int L = 1;
        while (L < txt.Length)
        {
            order = sortdouble(txt, L, order, cls);
            cls = updatecls(order, cls, L);
            L = 2 * L;
        }
        return order;
    }
    static bool bigger(string patt, int str, string txt)
    {

        for (int i = 0; i < Math.Min(patt.Length, txt.Length - str); i++)
        {
            if (patt[i] != txt[str + i])
            {
                return patt[i] > txt[str + i];
            }
        }
        return false;

    }
    static bool bigger2(string patt, int str, string txt)
    {

        for (int i = 0; i < Math.Min(patt.Length, txt.Length - str); i++)
        {
            if (patt[i] != txt[str + i])
            {
                return patt[i] < txt[str + i];
            }
        }
        return false;
    }
    static List<int> pattmatchwithsuffarr(string txt, string patt, int[] mysuff)
    {
        List<int> ansans = new List<int>()  ; 
        int minindex = 0;
        int maxindex = txt.Length;
        while (minindex < maxindex)
        {
            int midindex = (minindex + maxindex) / 2;
            int myindex = mysuff[midindex];
            bool random = bigger(patt, myindex, txt);
            if (random)
            {
                minindex = midindex + 1;
            }
            else
            {
                maxindex = midindex;
            }
        }
        int start = minindex;
        maxindex = txt.Length;
        while (minindex < maxindex)
        {
            int midindex = (minindex + maxindex) / 2;
            int myindex2 = mysuff[midindex];
            bool random = bigger2(patt, myindex2, txt);
            if (random)
            {
                maxindex = midindex;
            }
            else
            {
                minindex = midindex + 1;
            }
        }
        int end = maxindex;
        if (start <= end)
        {
            for (int i = start; i < end; i++)
            {
                ansans.Add(mysuff[i]);
            }
        }
        return ansans  ; 
    }

    static void Main()
    {
        string txt = Console.ReadLine() + '$';
        int n = int.Parse(Console.ReadLine());
        var patterns = Console.ReadLine().Split();
        var mysuffixarray = BuildSuffixArray(txt);
        List<int> mya = new List<int>()  ; 
        bool[] myarr = new bool[txt.Length];
        for (int i = 0; i < patterns.Length; i++)
        {
            string patt = patterns[i];
            var final = pattmatchwithsuffarr(txt, patt, mysuffixarray);
            foreach(var j in  final)
            {
                myarr[j]  = true ;  
            }
        }
            for(int ia = 0 ; ia < myarr.Length ; ia++)
            {
                if(myarr[ia])
                {
                    mya.Add(ia) ;
                }
            }
            Console.WriteLine(string.Join(" " , mya)) ; 
    }
}