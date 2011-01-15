/// Ali Moeeny : 2006-01-20 Started

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace NeuralData
{
    /// <summary>
    /// It is a container for clustering results, 
    /// Value is the information contnent or whatever measure which might represent 
    /// the value, strenght, distance ... of the cluster members
    /// </summary>
    public struct StimulusCluster
    {
        public int[] Members;
        public double Value;
    }
    
    
    /// <summary>
    /// Statistics
    /// </summary>
    ///  
    /// 
    public class StatisticalTests
    {
        public enum StatisticalTestTail{Left = 1, Both = 2, Right = 3};

        public static double Min(int[] a)
        {
            double r = double.MaxValue;
            for (int i = 0; i < a.Length; i++)
            {
                r = Math.Min(a[i], r);
            }
            return r;
        }

        public static double Min(double[] a)
        {
            double r = double.MaxValue;
            for (int i = 0; i < a.Length; i++)
            {
                r = Math.Min(a[i], r);
            }
            return r;
        }

        public static double Max(double[] a)
        {
            double r = double.MinValue;
            for (int i = 0; i < a.Length; i++)
            {
                r = Math.Max(a[i], r);
            }
            return r;
        }

        public static int Max(int[] a)
        {
            int r = int.MinValue;
            for (int i = 0; i < a.Length; i++)
            {
                r = Math.Max(a[i], r);
            }
            return r;
        }

        public static double Max(double[,] a, int dim)
        {
            double[] b = new double[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                b[i] = a[i, dim];
            }
            return Max(b);
        }


        public static double Sum(int[] a)
        {
            double s = 0;
            for (int i = 0; i < a.Length; i++)
                s += a[i];
            return s;
        }
        public static double Sum(bool[] a)
        {
            double s = 0;
            for (int i = 0; i < a.Length; i++)
                if(a[i]) s ++;
            return s;
        }

        public static double Sum(double[] a)
        {
            double s = 0;
            for (int i = 0; i < a.Length; i++)
                s += a[i];
            return s;
        }
        public static double Sum(double[,] a, int dim)
        {
            double s = 0;
            for (int i = 0; i < a.GetLength(0); i++)
                s += a[i,dim];
            return s;
        }
        public static double Mean(int[] a)
        {
            double m = 0;
            for (int i = 0; i < a.Length; i++)
            {
                m += a[i];
            }
            return (m / (double)a.Length);
        }
        public static double Mean(double[] a)
        {
            if (a == null) return 0.0d;
            if (a.Length == 0) return 0.0d;
            double m = 0;
            for (int i = 0; i < a.Length; i++)
            {
                m += a[i];
            }
            return (m / (double)a.Length);
        }
        public static double Mean(double[,] a, int dim)
        {
            double m = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                m += a[i,dim];
            }
            return (m / (double)a.GetLength(0));
        }

        public static double[] Mean(double[][] a)
        {
            a = Squeeze(a);
            double[] m = new double[a[0].GetLength(0)];
            double[] temp = new double[a.GetLength(0)];
            for (int i = 0; i < a[0].GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(0); j++)
                {
                   temp[j] = a[j][i];
                }
                m[i] = Mean(temp);
            }
            return m;
        }
        public static double[] Mean(double[][] a, int dim)
        {
            double[] r=new double[0];
            if (dim == 0)
            {
                r = new double[a.GetLength(0)];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    r[i] = Mean(a[i]);
                }
            }
            return r;
         
        }

        public static int Remove(ref int[] A, int a)
        {
            int Count = 0;
            int[] temp = new int[0];
            if (A != null)
            {
                for (int i = 0; i < A.GetLength(0); i++)
                    if (A[i] != a)
                    {
                        Array.Resize(ref temp, temp.GetLength(0) + 1);
                        temp[temp.GetLength(0) - 1] = A[i];
                    }
                    else Count++;
                A = temp;
            }
            return Count;
        }

        /// <summary>
        /// bias-corrected sample variance" 
        /// </summary>
        /// <param name="a">The vector</param>
        /// <returns>a DOUBLE as the bias-corrected (/N-1) estimate of popolation variance</returns>
        public static double Variance(int[] a)
        {
            // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
            double v = 0;
            double m = Mean(a);
            for (int i = 0; i < a.Length; i++)
            {
                v += Math.Pow((a[i] - m),2);
            }
            return (v / (double)(a.Length-1.0d));
        }

        public static double Variance(double[] a)
        {
            if (a == null) return 0;
            if (a.Length < 2) return 0.0d;
            // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
            double v = 0;
            double m = Mean(a);
            for (int i = 0; i < a.Length; i++)
            {
                v += Math.Pow((a[i] - m), 2);
            }
            return (v / (double)(a.Length - 1.0d));
        }
        public static double Variance(double[,] a, int dim)
        {
            // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
            double v = 0;
            double m = Mean(a,dim);
            for (int i = 0; i < a.GetLength(0); i++)
            {
                v += Math.Pow((a[i,dim] - m), 2);
            }
            return (v / (double)(a.GetLength(0) - 1.0d));
        }
        public static double[] Variance(double[][] a)
        {
            double[] r = new double[a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
                r[i] = Variance(a[i]);
            return r;
        }

        public static double StandardError(int[] a)
        {
            if (a == null) return 0;
            if (a.Length < 1) return 0;
            double se = 0;
            se = Math.Sqrt(Variance(a)) / Math.Sqrt(a.Length);
            return se;
        }
        public static double StandardError(double[] a)
        {
            if (a == null) return 0;
            if (a.Length < 1) return 0;
            double se = 0;
            se = Math.Sqrt(Variance(a)) / Math.Sqrt(a.Length);
            return se;
        }
        public static double StandardError(double[,] a, int dim)
        {
            if (a == null) return 0;
            if (a.GetLength(0) < 1) return 0;
            if (a.Length < 1) return 0;
            double se = 0;
            se = Math.Sqrt(Variance(a,dim)) / Math.Sqrt(a.GetLength(0));
            return se;
        }

        public static double[] NormalizeStreachZero2One(double[] a)
        {
            double min = Min(a);
            for (int i = 0; i < a.GetLength(0); i++)
            {
                a[i] = a[i] - min;
            }
            return Normalize2Max(a);
        }

        public static double[] Normalize2Max(double[] a)
        {
            double m = Max(a);
            if (m !=0)
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = a[i] / m;
                }
            return a;
        }

        public static double[] Normalize(double[] a, double NormalizeMax)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = a[i] / NormalizeMax;
            }
            return a;
        }

        public static double Probability(double[] P, double s)
        {
            double r = 0;
            for (int i = 0; i < P.Length; i++)
                if (P[i] == s) r++;
            return (r/P.Length);
        }

        public static double TCDF(double a, double DegreesofFreedom)//Student's t cumulative distribution function
        {
            double pValue;
            int NormCutoff = 10000000;
            
            //First compute F(-|x|).
            // Cauchy distribution.  See Devroye pages 29 and 450.
            //cauchy = (v == 1);
            //p(cauchy) = .5 + atan(x(cauchy))/pi;
            pValue = 0.5f + Math.Atan(a)/Math.PI;

            // Normal Approximation.
            if (DegreesofFreedom > NormCutoff)
                pValue = normcdf(a);

            // See Abramowitz and Stegun, formulas 26.5.27 and 26.7.1
            //gen = ~(cauchy | normal | nans);
            pValue = IncompleteBeta(DegreesofFreedom/2.0f, 0.5f, DegreesofFreedom / (DegreesofFreedom + Math.Pow(a,2))) / 2.0f;

            // Reflect if necessary.
            //reflect = gen & (x > 0);
            //p(reflect) = 1 - p(reflect);

            // Make the result exact for the median.
//            p(x == 0 & ~nans) = 0.5;
            return pValue;
        }

        public static double FCDF(double x, int v1, int v2)
        {
            double pValue=0;
            // v1 and v2 should be positive numbers 
            //t = (v1 <= 0 | v2 <= 0 | isnan(x) | isnan(v1) | isnan(v2));
            //p(t) = NaN;
            //s = (x==Inf) & ~t;
            //if any(s(:))
            //   p(s) = 1;
            //   t = t | s;
            //end
            double xx = (double)x /(double)(x + (double)v2/(double)v1);
            
            if((x>0)) // && (!v1.IsNaN) && (!v2.IsNan)  )
                pValue = IncompleteBeta(v1/2.0d, v2/2.0d, xx);
            //The following commented pValue calculaions are to be used when v1 or v2 or both are NaN
            //pValue = chi2cdf(v1*x,v1); // v2 is NaN
            //pValue = 1 - chi2cdf(v2/x,v2); // v1 is NaN
            //pValue = (x>=1); // v1 and v2 are Nan

            return pValue;
        }

        /// <summary>
        /// Returns the incomplete beta function Ix(a, b).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double IncompleteBeta(double a, double b, double x)
        {
            //float betacf(float a, float b, float x);
            //float GammaLn(float xx);
            double bt;
            if (x < 0.0 || x > 1.0) throw new Exception("Bad x in routine betai");
            if (x == 0.0 || x == 1.0) bt=0.0;
            else //Factors in front of the continued fraction.
                bt=Math.Exp(GammaLn(a+b)-GammaLn(a)-GammaLn(b)+a*Math.Log(x)+b*Math.Log(1.0-x));
            if (x < (a+1.0)/(a+b+2.0)) //Use continued fraction directly.
            return bt*BetaContinuedFraction(a,b,x)/a;
            else //Use continued fraction after making the symmetry transformation
            return 1.0-bt*BetaContinuedFraction(b,a,1.0-x)/b;
        }

 
        ///Used by Incomplete Beta: Evaluates continued fraction for incomplete beta function by modified Lentz’smethod (Chapter 5.2, Numerical Recepies).
        public static double BetaContinuedFraction(double a, double b, double x)
        {
            double MAXIT = 100; // Math.Sqrt(Math.Max(a,b)); this would be enough but the problem araises when x is NaN
            double EPS = 3.0e-7;
            double FPMIN = 1.0e-30;

            int m,m2;
            double aa,c,d,del,h,qab,qam,qap;
            qab=a+b; //These q’s will be used in factors that occur in the coefficients (6.4.6).
            qap = a + 1.0;
            qam=a-1.0;
            c=1.0; //First step of Lentz’s method.
            d=1.0-qab*x/qap;
            if (Math.Abs(d) < FPMIN) d=FPMIN;
            d=1.0/d;
            h=d;
            for (m=1;m<=MAXIT;m++) 
            {
                m2=2*m;
                aa=m*(b-m)*x/((qam+m2)*(a+m2));
                d=1.0+aa*d; //One step (the even one) of the recurrence.
                if (Math.Abs(d) < FPMIN) d = FPMIN;
                c=1.0+aa/c;
                if (Math.Abs(c) < FPMIN) c = FPMIN;
                d=1.0/d;
                h *= d*c;
                aa = -(a+m)*(qab+m)*x/((a+m2)*(qap+m2));
                d=1.0+aa*d; //Next step of the recurrence (the odd one).
                if (Math.Abs(d) < FPMIN) d = FPMIN;
                c=1.0+aa/c;
                if (Math.Abs(c) < FPMIN) c = FPMIN;
                d=1.0/d;
                del=d*c;
                h *= del;
                if (Math.Abs(del - 1.0) < EPS) break; //Are we done?
            }
            if (m > MAXIT) throw new Exception("a or b too big, or MAXIT too small in betacf. Max ierations:"+MAXIT.ToString());
            return h;
        }

        public static double GammaLn(double xx) //Returns the value ln[Γ(xx)] for xx > 0.
        {
            //Internal arithmetic will be done in double precision, a nicety that you can omit if five-figure
            //accuracy is good enough.
            double x,y,tmp,ser;
            double[] cof = new double[6] {76.18009172947146,-86.50532032941677,24.01409824083091,-1.231739572450155,0.1208650973866179e-2,-0.5395239384953e-5};
            int j;
            y=x=xx;
            tmp=x+5.5;
            tmp -= (x+0.5)*Math.Log(tmp);
            ser=1.000000000190015;
            for (j=0;j<=5;j++) ser += cof[j]/++y;
            return -tmp+Math.Log(2.5066282746310005*ser/x);
        }

        public static double normcdf(double a)
        {
            throw new Exception("The method or operation normcdf is not implemented.");
        }


        public static double tTest2Samples(int[] p1, int[] p2)
        {
            return tTest2Samples(p1, p2, 0.05f, StatisticalTestTail.Both, true);
        }

        public static double tTest2Samples(int[] p1, int[] p2, Single Alpha, StatisticalTestTail Tail, bool EqualVarianceAssumed)
        {
            double pValue = 0;
            if ((Alpha>1)||(Alpha<0)) throw new Exception("Wronge Alpha for tTest: " + Alpha.ToString());

            double DegreeofFreedom = 0;
            double sPooled = 0;
            double p1Variance = 0;
            double p2Variance = 0;
            double se = 0;
            double ratio = 0;
            double difference = 0;
            difference = Mean(p1) - Mean(p2);
            p1Variance = Variance(p1);
            p2Variance = Variance(p2);
            
            // Ali: I added this to prevent devide by zero error in zero vaiances
            if (p1Variance == 0) p1Variance = 0.000001f;
            if (p2Variance == 0) p2Variance = 0.000001f;
            // Ali

            if (EqualVarianceAssumed) 
            {
                DegreeofFreedom = p1.Length + p2.Length - 2;
                sPooled = Math.Sqrt(((p1.Length-1) * p1Variance + (p2.Length-1) * p2Variance) / DegreeofFreedom);
                se = sPooled * Math.Sqrt(1.0f/p1.Length + 1.0f/p2.Length);
                ratio = difference / se;

            }
            else // Not EqualVarianceAssumed
            {
                double P1VBar = p1Variance / p1.Length;
                double P2VBar = p2Variance / p2.Length;
                DegreeofFreedom = Math.Pow((P1VBar + P2VBar),2) / (Math.Pow(P1VBar,2) / (p1.Length-1) + Math.Pow(P2VBar,2) / (p2.Length-1));
                se = Math.Sqrt(P1VBar + P2VBar);
                ratio = difference / se;
            }

            // Compute the correct p-value for the test, and confidence intervals if requested.
            switch(Tail)
            {
                case StatisticalTestTail.Both : 
                    pValue = 2 * TCDF(-Math.Abs(ratio),DegreeofFreedom);
                    //spread = tinv(1 - alpha ./ 2, DegreeofFreedom) .* se;
                    //ci = [(difference - spread) (difference + spread)];
                    break;
                case StatisticalTestTail.Left :
                    pValue = TCDF(ratio,DegreeofFreedom);
                    //    if nargout > 2
                    //        spread = tinv(1 - alpha, dfe) .* se;
                    //        ci = [-Inf, (difference + spread)];
                    break;
                case StatisticalTestTail.Right:
                    pValue = TCDF(-ratio,DegreeofFreedom);
                    //    if nargout > 2
                    //        spread = tinv(1 - alpha, dfe) .* se;
                    //        ci = [(difference - spread), Inf];
                    break;
            }

            // Determine if the actual significance exceeds the desired significance
            //if (p <= alpha)
            //    h = 1;
            //else p > alpha
            //    h = 0;
            //else // isnan(p) must be true
            //    h = NaN;
            //end

            return pValue;
        }

        public static double tTest1Sample(double[] p, double m, Single Alpha, StatisticalTestTail Tail)
        {
            if ((Alpha > 1) || (Alpha < 0)) throw new Exception("Wronge Alpha for tTest: " + Alpha.ToString());
            double pValue = 0;
            
            double xmean = Mean(p);
            double STDp = Math.Sqrt(Variance(p));
            double ser = STDp / Math.Sqrt(p.Length);
            double tval = (xmean - m) / ser;
   
            // Compute the correct p-value for the test, and confidence intervals if requested.
            switch(Tail)
            {
                case StatisticalTestTail.Both:
                pValue = 2 * TCDF(-Math.Abs(tval), p.Length - 1);
                //if nargout > 2
                //    crit = tinv((1 - alpha / 2), samplesize - 1) .* ser;
                //    ci = [(xmean - crit) (xmean + crit)];
                    break;
                case StatisticalTestTail.Left:
                    pValue = TCDF(-tval, p.Length - 1);
                //if nargout > 2
                //    crit = tinv(1 - alpha, samplesize - 1) .* ser;
                //    ci = [(xmean - crit), Inf];
                    break;
                case StatisticalTestTail.Right:
                    pValue = TCDF(tval, p.Length - 1);
                //if nargout > 2
                //    crit = tinv(1 - alpha, samplesize - 1) .* ser;
                //    ci = [-Inf, (xmean + crit)];
                    break;
            }
            //// Determine if the actual significance exceeds the desired significance
            //if p <= alpha
            //    h = 1;
            //elseif p > alpha
            //    h = 0;
            //else // isnan(p) must be true
            //    h = NaN;
            //end

            return pValue;
        }

        public static double tTestPaired(int[] p1, int[] p2, Single Alpha, StatisticalTestTail Tail)
        {
            double[] p11 = new double[p1.GetLength(0)];
            double[] p22 = new double[p2.GetLength(0)];
            for (int i = 0; i < p1.GetLength(0); i++)
            {
                p11[i] = (double)p1[i];
                p22[i] = (double)p2[i];
            }
            return tTestPaired(p11, p22, Alpha, Tail);
        }
        public static double tTestPaired(double[] p1, double[] p2, Single Alpha, StatisticalTestTail Tail)
        {
            double pValue=0;
            if ((Alpha>1)||(Alpha<0)) throw new Exception("Wronge Alpha for tTest: " + Alpha.ToString());
            if(p1.Length!=p2.Length) throw new Exception("In a paired tTest two samples must have equal length!");
            for (int i = 0; i < p1.GetLength(0); i++)
            {
                p1[i] = p1[i] - p2[i];
            }
            int m = 0;
            int samplesize  = p1.Length;
            int df = samplesize - 1;
            double xmean = Mean(p1);
            double sdpop = Math.Sqrt(Variance(p1));
            //Ali added this to avoid division by zero
            if (sdpop == 0) sdpop = Double.MinValue;
            
            double ser = sdpop / Math.Sqrt(samplesize);
            double tval = (xmean - m) / ser;
            switch (Tail)
            {
                case StatisticalTestTail.Both:
                    pValue = 2 * TCDF(-Math.Abs(tval), df);
                    //if nargout > 2
                    //    crit = tinv((1 - alpha / 2), samplesize - 1) .* ser;
                    //    ci = [(xmean - crit) (xmean + crit)];
                    //end
                    break;
                case StatisticalTestTail.Right:
                    pValue = TCDF(-tval, df);
                    //if nargout > 2
                    //    crit = tinv(1 - alpha, samplesize - 1) .* ser;
                    //    ci = [(xmean - crit), Inf];
                    //end
                    break;
                case StatisticalTestTail.Left:
                    pValue = TCDF(tval, df);
                    //if nargout > 2
                    //    crit = tinv(1 - alpha, samplesize - 1) .* ser;
                    //    ci = [-Inf, (xmean + crit)];
                    //end
                    break;
            }
            


            //if (((xmean-m) == 0) && (ser == 0)) { pValue = 10; return pValue; } // ALI: if there is no difference between means of two samples or sd is zero these are not different or at least test is impossible (2006-02-09 ASHOORA!)

            //// Determine if the actual significance exceeds the desired significance
            //if p <= alpha
            //    h = 1;
            //elseif p > alpha
            //    h = 0;
            //else // isnan(p) must be true
            //    h = NaN;
            //end
            return pValue;
        }

        public static double ANOVAoneway(double[] x1, double[] x2)
        {
            double[][] x = new double[2][];
            x[0] = x1;
            x[1] = x2;
            return ANOVAoneway(x);
        }
        /// <summary>
        /// This would perform Oneway ANOVA on the input matrix and return the pValue for "acceptance" of null hypothesis that the means of the groups are equal.
        /// </summary>
        /// <param name="x">The data matrix formated as double[][]</param>
        /// <returns>pValue for "rejection" of the null hypothesis that the means of the groups are equal</returns>
        public static double ANOVAoneway(double[][] x)
        {
            double pValue = 0;
            //It returns the p-value for the rejection of null hypothesis that the means of the groups are equal.

            x = Squeeze(x);
            int c = x.GetLength(0);
            int r = 0;
            int lx = 0;
            for (int i = 0; i < c; i++)
            {
                r = Math.Max(r, x[i].GetLength(0));
                lx += x[i].GetLength(0);
            }
            
            //int lx = r * c;
            //for (int i = 0; i < x.GetLength(0); i++)
            //{
            //    int cc = x[i].GetLength(0);
            //    if(cc<r)
            //    {
            //        double mt = Mean(x[i]);
            //        Array.Resize(ref x[i],r);
            //        for(int j = cc; j<r; j++) x[i][j] = mt;
            //    }
            //}

            double mu= 0.0d;
            for (int i = 0; i < x.GetLength(0); i++)
			{
                mu += Sum(x[i]);
			}
            mu = (double)mu / (double)lx;

            // center to improve accuracy
            for (int i = 0; i < x.GetLength(0); i++)
			{
                for (int j = 0; j < x[i].GetLength(0); j++)
			    {
                    x[i][j] -= mu;
			    }
			}


            double[] xm = new double[c];
            double gm = 0.0d;
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x[i].GetLength(0); j++)
                {
     		        xm[i] += x[i][j];
                    gm += x[i][j];
			    }
                xm[i] = xm[i] / (double)x[i].GetLength(0);
            }
            gm = (double)gm / (double)lx;
            int df1 = c - 1;
            int df2 = lx - df1 - 1 ;

            double RSS = 0;
            for (int i = 0; i < xm.GetLength(0); i++)
            {
                RSS += x[i].GetLength(0) * (xm[i] - gm) * (xm[i] - gm);
            }
            

            double TSS = 0;
            for (int i = 0; i < x.GetLength(0); i++)
                for(int j = 0; j < x[i].GetLength(0); j++)
                    TSS += (x[i][j] - gm) * (x[i][j] - gm);
                
            double SSE = TSS - RSS;

            double mse = 0;
            if (df2 > 0)
                mse = SSE / (double)df2;
            else
                mse = double.NaN;

            double F = 0;
            if (SSE != 0)
            {
                F = (RSS / df1) / mse;
                pValue = 1 - FCDF(F, df1, df2);
            }
            else if (RSS == 0)
            {
                F = double.NaN;
                pValue = double.NaN;
            }

            return pValue;
        }

        public static void MultipleComparisons(double[] x1, double[] x2)
        {
        }
        public static void MultipleComparisons(double[][] x)
        {
            double[] GroupMeans = new double[x.GetLength(0)];
            for (int i = 0; i < x.GetLength(0); i++) GroupMeans[i] = Mean(x[i]);

            int DegreeofFreedom = 0;

            //crit = getcrit(ctype, alpha, df, ng);
               
            //gcov = diag((s^2)./n);

        }


        public enum MultiCompSource {ANOVA1=1, ANOVA2=2, ANOVAN=3, AOCTool=4, KruskalWallis=5, Friedman=6};
        public enum MultiCompType {Tukey_Kramer=1, Dunn_Sidak=2, Bonferroni=3, LSD=4, Scheffe=5, HSD=6};

        public static void MultipleComparisons(/*comparison,means,h,*/ MultiCompSource Source, double[] GroupMeans, string[] GroupNames,int[] GroupMemberCounts, int DegreesofFreedom, double sqrtMSE, Single alpha, MultiCompType ctype /*, estimate ,dimension*/)
        {
#region Matlab comment
//            function [comparison,means,h,gnames] = multcompare(stats,varargin)
//%MULTCOMPARE Perform a multiple comparison of means or other estimates
//%   MULTCOMPARE performs a multiple comparison using one-way anova or
//%   anocova results to determine which estimates (such as means,
//%   slopes, or intercepts) are significantly different.
//%
//%   COMPARISON = MULTCOMPARE(STATS) performs a multiple comparison
//%   using a STATS structure that is obtained as output from any of
//%   the following functions:  anova1, anova2, anovan, aoctool,
//%   kruskalwallis, friedman.  The return value COMPARISON is a matrix
//%   with one row per comparison and five columns.  Columns 1-2 are the
//%   indices of the two samples being compared.  Columns 3-5 are a lower
//%   bound, estimate, and upper bound for their difference.
//%
//%   COMPARISON = MULTCOMPARE(STATS, 'PARAM1',val1, 'PARAM2',val2,...)
//%   specifies one or more of the following name/value pairs:
//%   
//%     'alpha'       Specifies the confidence level as 100*(1-ALPHA)%
//%                   (default 0.05).
//%     'displayopt'  Either 'on' (the default) to display a graph of the
//%                   estimates with comparison intervals around them, or
//%                   'off' to omit the graph.
//%     'ctype'       The type of critical value to use.  Choices are
//%                   'tukey-kramer' (default), 'dunn-sidak', 'bonferroni',
//%                   'scheffe'.  Enter two or more choices separated by
//%                   spaces, to use the minimum of those critical values.
//%     'dimension'   A vector specifying the dimension or dimensions over
//%                   which the population marginal means are to be
//%                   calculated.  Used only if STATS comes from anovan.
//%                   The default is to compute over the first dimension
//%                   associated with a categorical (non-continuous) factor.
//%                   The value [1 3], for example, computes the population
//%                   marginal mean for each combination of the first and
//%                   third predictor values.
//%     'estimate'    Estimate to compare.  Choices depend on the source of
//%                   the stats structure:
//%         anova1:  ignored, compare group means
//%         anova2:  'column' (default) or 'row' means
//%         anovan:  ignored, compare population marginal means
//%         aoctool:  'slope', 'intercept', or 'pmm' (default is 'slope'
//%                   for separate-slopes models, 'intercept' otherwise)
//%         kruskalwallis:  ignored, compare average ranks of columns
//%         friedman:  ignored, compare average ranks of columns
//%
//%   [COMPARISON,MEANS,H,GNAMES] = MULTCOMPARE(...) returns additional
//%   outputs.  MEANS is a matrix with columns equal to the estimates
//%   and their standard errors.  H is a handle to the figure containing
//%   the graph.  GNAMES is a cell array with one row for each group,
//%   containing the names of the groups.
//%
//%   The intervals shown in the graph are computed so that to a very close
//%   approximation, two estimates being compared are significantly different
//%   if their intervals are disjoint, and are not significantly different if
//%   their intervals overlap.  (This is exact for multiple comparison
//%   of means from anova1, if all means are based on the same sample size.)
//%   You can click on any estimate to see which means are significantly
//%   different from it.
//%
//%   Two additional CTYPE choices are available.  The 'hsd' option stands
//%   for "honestly significant differences" and is the same as the
//%   'tukey-kramer' option.  The 'lsd' option stands for "least significant
//%   difference" and uses plain t-tests; it provides no protection against
//%   the multiple comparison problem unless it follows a preliminary overall
//%   test such as an F test.
//%
//%   MULTCOMPARE does not support multiple comparisons using anovan output
//%   for a model that includes random or nested effects.  The calculations
//%   for a random effects model produce a warning that all effects are
//%   treated as fixed.  Nested models are not accepted.
//%
//%   Example:  Perform 1-way anova, and display group means with their names
//%
//%      load carsmall
//%      [p,t,st] = anova1(MPG,Origin,'off');
//%      [c,m,h,nms] = multcompare(st,'display','off');
//%      [nms num2cell(m)]   
//%
//%   See also ANOVA1, ANOVA2, ANOVAN, AOCTOOL, FRIEDMAN, KRUSKALWALLIS.

//%   Also supports older calling sequence:
//%      [...] = MULTCOMPARE(STATS,ALPHA,DISPLAYOPT,CTYPE,ESTIMATE,DIM)
//%
//%   Reference: Y. Hochberg and A.C. Tamhane, "Multiple Comparison
//%   Procedures," Wiley, New York, 1987.
//%
//%   The Tukey-Kramer critical value is the default.  This is known
//%   to be the best choice for one-way anova comparisons, but the
//%   conjecture that this is best for other comparisons is
//%   unproven.  The Bonferroni and Scheffe critical values are
//%   conservative in all cases.

//%   Copyright 1993-2005 The MathWorks, Inc. 
//%   $Revision: 1.9.4.5 $  $Date: 2005/11/18 14:28:14 $
#endregion

            if ((alpha<=0) || (alpha>=1))
                new Exception("Alpha for Multiple Comparisons analysis should be between 1 and 0");

            switch(Source)
            {
                case MultiCompSource.ANOVA1:
                   //GroupMemberCounts <- n = stats.n(:);
                   //sqrtMSE <- s = stats.s;
                   int ng = GroupMemberCounts.GetLength(0);
                   if (DegreesofFreedom < 1)
                      new Exception("stats:multcompare:NotEnoughData Cannot compare means with 0 degrees of freedom for error.");
                   
                   double crit = GetCriticalValues( ctype, alpha, DegreesofFreedom, ng);
                   double[] gcov = new double[ng];
                    for(int i = 0; i<ng; i++) gcov[i] = Math.Pow(sqrtMSE,2)/GroupMemberCounts[i];
                    break;
#region ANOVA2
                case MultiCompSource.ANOVA2:
                //   docols = logical(1);
                //   if (~isempty(estimate))
                //      if ~(   isequal(estimate,'row') || isequal(estimate,'column')...
                //           || isequal(estimate,'r')   || isequal(estimate,'c'))
                //         error('stats:multcompare:BadEstimate',...
                //               'ESTIMATE must be ''column'' or ''row''.');
                //      end
                //      docols = isequal(estimate, 'column') || isequal(estimate, 'c');
                //   end
                //   if (docols)
                //      gmeans = stats.colmeans(:);
                //      n = stats.coln(:);
                //      mname = 'column means';
                //   else
                //      gmeans = stats.rowmeans(:);
                //      n = stats.rown(:);
                //      mname = 'row means';
                //   end
                //   ng = length(gmeans);
                //   sigma = sqrt(stats.sigmasq);
                //   gnames = strjust(num2str((1:ng)'), 'left');
                //   df = stats.df;
                //   if (df < 1)
                //      error('stats:multcompare:NotEnoughData',...
                //            'Cannot compare means with 0 degrees of freedom for error.');
                //   end
                   
                //   % Get Tukey-Kramer critical value
                //   if (isempty(ctype)), ctype = 'tukey-kramer'; end
                //   crit = getcrit(ctype, alpha, df, ng);
                   
                //   gcov = ((sigma^2)/n) * eye(ng);

                //   % This whole activity is a little strange if the model includes
                //   % interactions, especially if they are important.
                //   if (stats.inter && dodisp)     % model included an interaction term
                //      if (stats.pval < alpha)
                //         disp(sprintf(...
                //['Note:  Your model includes an interaction term that is significant\n'...
                // 'at the level you specified.  Testing main effects under these\n'...
                // 'conditions is questionable.']));
                //      else
                //         disp(sprintf(...
                //['Note:  Your model includes an interaction term.  A test of main\n'...
                // 'effects can be difficult to interpret when the model includes\n'...
                // 'interactions.']));
                //      end
                //   end
                    break;
#endregion
#region ANOVAN
                case MultiCompSource.ANOVAN:
                       //mname = 'population marginal means';
                       
                       //% We do not handle nested models
                       //try
                       //    vnested = stats.vnested;
                       //catch
                       //    vnested = [];
                       //end
                       //if any(vnested(:))
                       //    error('stats:multcompare:NoNesting',...
                       //          'MULTCOMPARE does not support models with nested factors.');
                       //end


                       //% Our calculations treat all effects as fixed
                       //if ~isempty(stats.ems)
                       //   warning('stats:multcompare:IgnoringRandomEffects',...
                       //           'Ignoring random effects (all effects treated as fixed).')
                       //end
                       
                       //nvars = length(stats.nlevels);
                       //P0 = stats.nullproject;
                       
                       //% Make sure DIM is a scalar or vector of factor numbers.
                       //if isempty(dim)
                       //    dim = find(stats.nlevels>1,1);
                       //end
                       //dim = dim(:);
                       //if isempty(dim) || any(dim<1 | dim>nvars | dim~=round(dim))
                       //   error('stats:multcompare:BadDim',...
                       //         'Values of DIM must be integers between 1 and %d.',nvars);
                       //end
                       //dim = sort(dim);
                       //dim(diff(dim)==0) = [];
                       //if any(stats.nlevels(dim)<2)
                       //   error('stats:multcompare:BadDim',...
                       //         'DIM must specify only categorical factors with 2 or more degrees of freedom.');
                       //end
                       
                       //% Create all combinations of the specified factors
                       //try
                       //    continuous = stats.continuous;
                       //catch
                       //    continuous = zeros(nvars,1);
                       //end
                       //ffdesign = fullfact(stats.nlevels(dim));
                       //nrows = size(ffdesign,1);
                       
                       //% Create a design matrix for these combinations
                       //dums = cell(nvars, 1);
                       //for j=1:length(dim)
                       //   dj = dim(j);
                       //   dums{dj} = idummy(ffdesign(:,j),3);  % dummy variables for each factor
                       //end
                       
                       //% Create a vector of average values for remaining factors
                       //for j=1:nvars
                       //   if isempty(dums{j});
                       //       if continuous(j)
                       //          dums{j} = repmat(stats.vmeans(j),nrows,1);
                       //       else
                       //          nlev = stats.nlevels(j);
                       //          dums{j} = repmat(1/nlev,nrows,nlev);
                       //       end
                       //   end
                       //end

                       //% Fill in x columns for each term
                       //termcols = stats.termcols(:);
                       //termstart = cumsum([1; termcols]);
                       //terms = [zeros(1,nvars); stats.terms];
                       //ncols = sum(termcols);
                       //x = zeros(size(ffdesign,1), ncols);
                       //x(:,1) = 1;
                       //for j=1:length(termcols)
                       //   tm = terms(j,:);
                       //   t0 = termstart(j);
                       //   t1 = termstart(j) + termcols(j) - 1;
                       //   if all(tm==0)
                       //      x(:,t0:t1) = 1;
                       //   else
                       //      x0 = [];
                       //      for k=nvars:-1:1
                       //         if tm(k)
                       //            x0 = termcross(x0,dums{k});
                       //         end
                       //      end
                       //      x(:,t0:t1) = x0;
                       //   end
                       //end

                       //% Compute estimates and their standard errors
                       //gmeans = x * stats.coeffs;
                       //xproj = (x*P0)';
                       //tmp = stats.Rtr \ xproj;
                       //if (stats.dfe == 0)
                       //   mse = NaN;
                       //else
                       //   mse = max(stats.mse,0);
                       //end
                       //gcov = mse * tmp' * tmp;
                       
                       //% Find non-estimable means and set them to NaN
                       //Xrows = stats.rowbasis';           % row basis of original X matrix
                       //bb = Xrows \ (x');                 % fit rows of x to row basis
                       //xres = Xrows * bb - x';            % get residuals
                       //xres = sum(abs(xres));             % sum of absolute residuals
                       //cutoff = sqrt(eps(class(xres))) * size(xres,2); % cutoff for large residuals
                       //gmeans(xres > cutoff) = NaN;       % not in row space of original X
                       
                       //% Get Tukey-Kramer critical value
                       //if (isempty(ctype)), ctype = 'tukey-kramer'; end
                       //crit = getcrit(ctype, alpha, stats.dfe, length(gmeans));


                       //% Get names for each group
                       //ngroups = size(ffdesign,1);
                       //gnames = cell(ngroups,1);
                       //allnames = stats.grpnames;
                       //varnames = stats.varnames;
                       //for j=1:ngroups
                       //   v1 = dim(1);
                       //   vals1 = allnames{v1};
                       //   nm = sprintf('%s=%s',varnames{v1},vals1{ffdesign(j,1)});
                       //   for i=2:size(ffdesign,2)
                       //      v2 = dim(i);
                       //      vals2 = allnames{v2};
                       //      nm = sprintf('%s,%s=%s',nm,varnames{v2},vals2{ffdesign(j,i)});
                       //   end
                       //   gnames{j} = nm;
                       //end
                        break;
#endregion

#region AOCTool
                case MultiCompSource.AOCTool:
                       //model = stats.model;
                       //if (model==1 || model==3)
                       //   error('stats:multcompare:NoMultipleParameters',...
                       //         'No multiple comparisons possible from this aoctool fit.');
                       //end
                       //gnames = stats.gnames;
                       //n = stats.n(:);
                       //ng = length(n);
                       //df = stats.df;
                       //if (df < 1)
                       //   error('stats:multcompare:NotEnoughData',...
                       //         'Cannot do multiple comparison with 0 d.f. for error.');
                       //end

                       //% Get either slope or intercept estimates and covariances
                       //if (isempty(estimate))
                       //   if (model == 5)
                       //      estimate = 'slope';
                       //   else
                       //      estimate = 'intercept';
                       //   end
                       //end
                       
                       //if (isequal(estimate,'s')), estimate = 'slope'; end
                       //if (isequal(estimate,'i')), estimate = 'intercept'; end
                       //if (isequal(estimate,'p')), estimate = 'pmm'; end
                       //if (~isempty(estimate))
                       //   if ~(   isequal(estimate,'slope') || isequal(estimate,'pmm')...
                       //        || isequal(estimate,'intercept'))
                       //      error('stats:multcompare:BadEstimate',...
                       //            'ESTIMATE must be ''slope'', ''intercept'', or ''pmm''.');
                       //   end
                       //end

                       //switch(estimate)
                       // case 'slope'
                       //   if (~isfield(stats, 'slopes'))
                       //      error('stats:multcompare:BadStats',...
                       //            'No slope estimates available in STATS argument.');
                       //   end
                       //   gmeans = stats.slopes;
                       //   gcov = stats.slopecov;
                       //   mname = 'slopes';
                       // case 'intercept'
                       //   if (~isfield(stats, 'intercepts'))
                       //      error('stats:multcompare:BadStats',...
                       //            'No intercept estimates available in STATS argument.');
                       //   end
                       //   gmeans = stats.intercepts;
                       //   gcov = stats.intercov;
                       //   mname = 'intercepts';
                       // case 'pmm'
                       //   gmeans = stats.pmm;
                       //   gcov = stats.pmmcov;
                       //   mname = 'population marginal means';
                       //end

                       //if (any(any(isinf(gcov))))
                       //   error('stats:multcompare:InfiniteVariance',...
                       //         'Cannot do comparisons.  Some %s have infinite variance.', mname);
                       //end
                       
                       //% Get Tukey-Kramer critical value
                       //if (isempty(ctype)), ctype = 'tukey-kramer'; end
                       //crit = getcrit(ctype, alpha, df, ng);
                    break;
#endregion

#region KruskalWallis

                case MultiCompSource.KruskalWallis:
                   //gmeans = stats.meanranks(:);
                   //gnames = stats.gnames;
                   //n = stats.n(:);
                   //sumt = stats.sumt;
                   //ng = length(n);
                   //N = sum(n);
                   //mname = 'mean ranks';

                   //% Get critical value; H&T recommend the Tukey-Kramer value
                   //if (isempty(ctype)), ctype = 'tukey-kramer'; end
                   //crit = getcrit(ctype, alpha, Inf, ng);
                   
                   //gcov = diag(((N*(N+1)/12) - (sumt/(12*(N-1)))) ./ n);
                   
                   //% Note that the intervals in M can be used for testing but not
                   //% for simultaneous confidence intervals.  See H&T, p. 249.
                   //if (dodisp)
                   //   disp(['Note:  Intervals can be used for testing but are not ' ...
                   //         'simultaneous confidence intervals.']);
                   //end
                    break;
#endregion

#region Friedman
                case MultiCompSource.Friedman:
                   //gmeans = stats.meanranks(:);
                   //n = stats.n;
                   //ng = length(gmeans);
                   //sigma = stats.sigma;
                   //mname = 'mean column ranks';
                   //gnames = strjust(num2str((1:ng)'), 'left');

                   //% Get critical value; H&T recommend the Tukey-Kramer value
                   //if (isempty(ctype)), ctype = 'tukey-kramer'; end
                   //crit = getcrit(ctype, alpha, Inf, ng);

                   //gcov = ((sigma^2) / n) * eye(ng);

                   //% Note that the intervals in M can be used for testing but not
                   //% for simultaneous confidence intervals.  See H&T, p. 249.
                   //if (dodisp)
                   //   disp(['Note:  Intervals can be used for testing but are not ' ...
                   //         'simultaneous confidence intervals.']);
                   //end
                    break;
#endregion
                    
 
            }//Switch 


//% Create output matrix showing tests for all pairwise comparisons
//% and graph that approximates these tests.
//[M,MM,hh] = makeM(gmeans, gcov, crit, gnames, mname, dodisp);

//comparison = M;
//if (nargout>1), means = MM; end
//if (nargout>2), h = hh; end
        }


        public static double GetCriticalValues(MultiCompType ctype, double alpha, int DegreeofFreedom, int ng)
        {
            //% Get the minimum of the specified critical values
            double crit = Double.MaxValue;
            double crit1 = Double.MaxValue;

            switch (ctype)
               {
                   //case MultiCompType.Tukey_Kramer | MultiCompType.HSD:
                   //  double crit1 = stdrinv(1-alpha, df, ng) / sqrt(2);
                     
                   //  // The T-K algorithm is inaccurate for small alpha, so compute
                   //  // an upper bound for it and make sure it's in range.
                   //  ub = GetCriticalValues(MultiCompType.Dunn_Sidak, alpha, df, ng);
                   //  if (crit1 > ub) crit1 = ub; 
                   //    break;

                   //case MultiCompType.Dunn_Sidak:
                   //  kstar = nchoosek(ng, 2);
                   //  alf = 1-Math.Pow((1-alpha),(1/kstar));
                   //  if (isinf(df))
                   //     crit1 = norminv(1-alf/2);
                   //  else
                   //     crit1 = tinv(1-alf/2, df);
                     
                   //    break;
                   case MultiCompType.Bonferroni:
                     double kstar = NChooseK(ng, 2);
                     if (double.IsInfinity(DegreeofFreedom))
                        crit1 = normsinv(1 - alpha / (2*kstar));
                     else
                        crit1 = tinv(1 - alpha / (2*kstar), DegreeofFreedom);
                     
                       break;
//                   case MultiCompType.LSD:
   //                  if (isinf(df))
   //                     crit1 = norminv(1 - alpha / 2);
   //                  else
   //                     crit1 = tinv(1 - alpha / 2, df);
   //                  end
   //                    break;

   //                case MultiCompType.Scheffe:
   //                  if (isinf(df))
   //                     tmp = chi2inv(1-alpha, ng-1) / (ng-1);
   //                  else
   //                     tmp = finv(1-alpha, ng-1, df);
   //                  end
   //                  crit1 = sqrt((ng-1) * tmp);
   //                   break; 
                   default:
                        new Exception("Not Implemented Yet");
                        break;
               }
   crit = Math.Min(crit, crit1);
   
               return crit;
        }

        private static double tinv(double p, int DegreesofFreedom)
        {
            #region Matlab 
            //function x = tinv(p,v);
            //%TINV   Inverse of Student's T cumulative distribution function (cdf).
            //%   X=TINV(P,V) returns the inverse of Student's T cdf with V degrees 
            //%   of freedom, at the values in P.
            //%
            //%   The size of X is the common size of P and V. A scalar input   
            //%   functions as a constant matrix of the same size as the other input.    
            //%
            //%   See also TCDF, TPDF, TRND, TSTAT, ICDF.

            //%   References:
            //%      [1]  M. Abramowitz and I. A. Stegun, "Handbook of Mathematical
            //%      Functions", Government Printing Office, 1964, 26.6.2

            //%   Copyright 1993-2005 The MathWorks, Inc.
            //%   $Revision: 2.11.2.7 $  $Date: 2005/12/12 23:34:13 $
            #endregion

            double r = 0;
            //% The inverse cdf of 0 is -Inf, and the inverse cdf of 1 is Inf.
            if ((p == 0) & (DegreesofFreedom > 0)) return Double.MinValue;
            if ((p == 1) & (DegreesofFreedom > 0)) return Double.MaxValue;

            //% Invert the Cauchy distribution explicitly
            if (DegreesofFreedom == 1)
                r = Math.Tan(Math.PI * (p - 0.5));

            else

                //% For small d.f., call betainv which uses Newton's method
                if (DegreesofFreedom < 1000)
                {
                    //double q = p - 0.5d;
                    //double z = betainv(1 - 2 * Math.Abs(q), DegreesofFreedom / 2, 0.5);
                    //r = Math.Sign(q) * Math.Sqrt(DegreesofFreedom / z - DegreesofFreedom);
                    new Exception("Not implemented yet!");
                }
                else
                    //% For large d.f., use Abramowitz & Stegun formula 26.7.5
                    //% k = find(p>0 & p<1 & ~isnan(x) & v >= 1000);
                    if (DegreesofFreedom >= 1000)
                    {
                        double xn = normsinv(p);
                        r = xn +
                            (Math.Pow(xn, 3) + xn) / (4 * DegreesofFreedom) +
                            (5 * Math.Pow(xn, 5) + 16 * Math.Pow(xn, 3) + 3 * xn) / (96 * Math.Pow(DegreesofFreedom, 2)) +
                            (3 * Math.Pow(xn, 7) + 19 * Math.Pow(xn, 5) + 17 * Math.Pow(xn, 3) - 15 * xn) / (384 * Math.Pow(DegreesofFreedom, 3)) +
                            (79 * Math.Pow(xn, 9) + 776 * Math.Pow(xn, 7) + 1482 * Math.Pow(xn, 5) - 1920 * Math.Pow(xn, 3) - 945 * xn) / (92160 * Math.Pow(DegreesofFreedom, 4));
                    }
            return r;
        }

//        private static double NormInv(double p)
//        {
//#region Matlab Help
////function [x,xlo,xup] = norminv(p,mu,sigma,pcov,alpha)
////%NORMINV Inverse of the normal cumulative distribution function (cdf).
////%   X = NORMINV(P,MU,SIGMA) returns the inverse cdf for the normal
////%   distribution with mean MU and standard deviation SIGMA, evaluated at
////%   the values in P.  The size of X is the common size of the input
////%   arguments.  A scalar input functions as a constant matrix of the same
////%   size as the other inputs.
////%
////%   Default values for MU and SIGMA are 0 and 1, respectively.
////%
////%   [X,XLO,XUP] = NORMINV(P,MU,SIGMA,PCOV,ALPHA) produces confidence bounds
////%   for X when the input parameters MU and SIGMA are estimates.  PCOV is a
////%   2-by-2 matrix containing the covariance matrix of the estimated parameters.
////%   ALPHA has a default value of 0.05, and specifies 100*(1-ALPHA)% confidence
////%   bounds.  XLO and XUP are arrays of the same size as X containing the lower
////%   and upper confidence bounds.
////%
////%   See also ERFINV, ERFCINV, NORMCDF, NORMFIT, NORMLIKE, NORMPDF,
////%            NORMRND, NORMSTAT.

////%   References:
////%      [1] Abramowitz, M. and Stegun, I.A. (1964) Handbook of Mathematical
////%          Functions, Dover, New York, 1046pp., sections 7.1, 26.2.
////%      [2] Evans, M., Hastings, N., and Peacock, B. (1993) Statistical
////%          Distributions, 2nd ed., Wiley, 170pp.

////%   Copyright 1993-2004 The MathWorks, Inc. 
////%   $Revision: 2.16.4.2 $  $Date: 2004/08/20 20:06:03 $
//#endregion

//    mu = 0;
//    sigma = 1;
//      alpha = 0.05;
////if ~isnumeric(alpha) || numel(alpha)~=1 || alpha<=0 || alpha>=1)     error('stats:norminv:BadAlpha ALPHA must be a scalar between 0 and 1.');

////% Return NaN for out of range parameters or probabilities.
////sigma(sigma <= 0) = NaN;
////p(p < 0 | 1 < p) = NaN;

//x0 = -Math.Sqrt(2)*erfcinv(2*p);

//x = sigma.*x0 + mu;

////% Compute confidence bounds if requested.
////if nargout>=2
////   xvar = pcov(1,1) + 2*pcov(1,2)*x0 + pcov(2,2)*x0.^2;
////   if any(xvar<0)
////      error('stats:norminv:BadCovariance',...
////            'PCOV must be a positive semi-definite matrix.');
////   end
////   normz = -norminv(alpha/2);
////   halfwidth = normz * sqrt(xvar);
////   xlo = x - halfwidth;
////   xup = x + halfwidth;
////end
//        }


        #region Jansen http://cbio.mskcc.org/~jansen/normsinv
        /*
 * Lower tail quantile for standard normal distribution function.
 *
 * This function returns an approximation of the inverse cumulative
 * standard normal distribution function.  I.e., given P, it returns
 * an approximation to the X satisfying P = Pr{Z <= X} where Z is a
 * random variable from the standard normal distribution.
 *
 * The algorithm uses a minimax approximation by rational functions
 * and the result has a relative error whose absolute value is less
 * than 1.15e-9.
 *
 * Author:      Peter J. Acklam
 * Time-stamp:  2002-06-09 18:45:44 +0200
 * E-mail:      jacklam@math.uio.no
 * WWW URL:     http://www.math.uio.no/~jacklam
 *
 * C implementation adapted from Peter's Perl version
 * by Chad Sprouse (http://home.online.no/~pjacklam/notes/invnorm/impl/sprouse/)
 * Adapted to C++ by Ronald Jansen
 */
   public static double normsinv(double p)
{
	double LOW = 0.02425;
	double HIGH = 0.97575;

	/* Coefficients in rational approximations. */
	double[] a = new double[]{		-3.969683028665376e+01,		 2.209460984245205e+02,		-2.759285104469687e+02,		 1.383577518672690e+02,		-3.066479806614716e+01,		 2.506628277459239e+00	};
	double[] b = new double[]{		-5.447609879822406e+01,		 1.615858368580409e+02,		-1.556989798598866e+02,		 6.680131188771972e+01,		-1.328068155288572e+01	};
	double[] c = new double[]{		-7.784894002430293e-03,		-3.223964580411365e-01,		-2.400758277161838e+00,		-2.549732539343734e+00,		 4.374664141464968e+00,		 2.938163982698783e+00	};
	double[] d = new double[]{		7.784695709041462e-03,		3.224671290700398e-01,		2.445134137142996e+00,		3.754408661907416e+00	};
	double q, r;
	int errno = 0;
	if (p < 0 || p > 1)
	{
		errno = 1;
		return 0.0;
	}
	else if (p == 0)
	{
		errno = 2;
        return Double.MinValue; /* minus "infinity" */ ;
	}
	else if (p == 1)
	{
		errno = 2;
        return Double.MaxValue; /* "infinity" */ ;
	}
	else if (p < LOW)
	{
		/* Rational approximation for lower region */
		q = Math.Sqrt(-2*Math.Log(p));
		return (((((c[0]*q+c[1])*q+c[2])*q+c[3])*q+c[4])*q+c[5]) /	((((d[0]*q+d[1])*q+d[2])*q+d[3])*q+1);
	}
	else if (p > HIGH)
	{
		/* Rational approximation for upper region */
		q  = Math.Sqrt(-2*Math.Log(1-p));
		return -(((((c[0]*q+c[1])*q+c[2])*q+c[3])*q+c[4])*q+c[5]) /	  ((((d[0]*q+d[1])*q+d[2])*q+d[3])*q+1);
	}
	else
	{
		/* Rational approximation for central region */
    		q = p - 0.5;
    		r = q*q;
		return (((((a[0]*r+a[1])*r+a[2])*r+a[3])*r+a[4])*r+a[5])*q /  (((((b[0]*r+b[1])*r+b[2])*r+b[3])*r+b[4])*r+1);
	}
}
        #endregion

        private static double NChooseK(int N, int K)
        {
            return (double)Factorial(N) / (double)(Factorial(K) * Factorial(N - K));
        }

        private static double Factorial(int N)
        {
            int f = 1;
            for (int i = 2; i < N; i++)
            {
                f *= i;
            }
            return f;
        }

        public static  void /*[M,MM,hh] =*/ makeM(/*gmeans, gcov, crit, gnames, mname, dodisp*/)
        {
            //% Create matrix to test differences, matrix of means, graph to display test
   
//ng = length(gmeans);
//MM = zeros(ng,2);
//MM(:,1) = gmeans(:);
//MM(:,2) = sqrt(diag(gcov));
//MM(isnan(MM(:,1)),2) = NaN;

//M = nchoosek(1:ng, 2);      % all pairs of group numbers
//M(1,5) = 0;                 % expand M to proper size
//g1 = M(:,1);
//g2 = M(:,2);
//M(:,4) = gmeans(g1) - gmeans(g2);
//i12 = sub2ind(size(gcov), g1, g2);
//gvar = diag(gcov);
//d12 = sqrt(gvar(g1) + gvar(g2) - 2 * gcov(i12));
//delta = crit * d12;
//M(:,3) = M(:,4) - delta;
//M(:,5) = M(:,4) + delta;

//% If requested, make a graph that approximates these tests
//if (dodisp)
//   % Find W values according to H&T (3.32, p. 98)
//   d = zeros(ng, ng);
//   d(i12) = d12;
//   sum1 = sum(sum(d));
//   d = d + d';
//   sum2 = sum(d);
//   if (ng > 2)
//      w = ((ng-1) * sum2 - sum1) ./ ((ng-1)*(ng-2));
//   else
//      w = repmat(sum1, 2, 1) / 2;
//   end
//   halfwidth = crit * w(:);
//   hh = meansgraph(gmeans, gmeans-halfwidth, gmeans+halfwidth, ...
//                   gnames, mname);
//   set(hh, 'Name', sprintf('Multiple comparison of %s',mname));
//else
//   hh = [];
//end

        }

        public static double[][] Squeeze(double[][] x)
        {
            double[][] r = new double[0][];
            if (x == null) return x;
            for (int i = 0; i < x.GetLength(0); i++)
            {
                if (x[i] != null)
                {
                    Array.Resize(ref r, r.GetLength(0) + 1);
                    r[r.GetLength(0) - 1] = x[i];
                }
            }
            return r;
        }

        public static double[][][] SqueezeAtEnd(double[][][] sq, double AssumeNull)
        {
            //for (int i = 0; i < sq.GetLength(0); i++)
            //    for (int j = 0; j < sq[i].GetLength(0); j++)
            //        for (int k = 0; k < sq[i][j].GetLength(0); k++) ;
            throw new Exception("Not implemented");
                    
                
              
        }

        public static double[] RemoveRepititions(double[] A)
        {
            double[] r = new double[0];
            for(int i = 0; i< A.Length; i++)
                if(Array.IndexOf(r,A[i])==-1)
                {
                    Array.Resize(ref r,r.Length+1);
                    r[r.Length-1]=A[i];
                }

            return r;
        }

        public static double[,] MinInfoClusterTree(bool[][][] NeuralSpace, int ResponseOnSet, int ResponseOffSet)
        {
            double[,] tree = new double[NeuralSpace.GetLength(0), 3];
            double[,] SimilarityMatrix = new double[NeuralSpace.GetLength(0), NeuralSpace.GetLength(0)];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
            {
                SimilarityMatrix = InformationSimilarityMatrix(NeuralSpace, ResponseOnSet, ResponseOffSet, false);
                
                //find the nearest nighbours
                int x = -1;
                int y = -1;
                double min = double.MaxValue;
                for (int j = 0; j < SimilarityMatrix.GetLength(0); j++)
                    for (int k = j + 1; k < SimilarityMatrix.GetLength(1); k++)
                        if (SimilarityMatrix[j, k] < min)
                        {
                            x = j;
                            y = k;
                            min = SimilarityMatrix[j, k];
                        } 
                
                MergeNighbors(ref NeuralSpace, x, y);


                tree[i,0] = x;
                tree[i,1] = y;
                tree[i,2] = min;
            }
            return tree;
        }

        private static void MergeNighbors(ref bool[][][] NeuralSpace, int x, int y)
        {
            if ((x == -1) || (y == -1)) return;
            bool [][] temp = new bool[NeuralSpace[x].GetLength(0)+NeuralSpace[y].GetLength(0)][];
            for (int i = 0; i < NeuralSpace[x].GetLength(0); i++)
                temp[i] = NeuralSpace[x][i];
            for (int i = 0; i < NeuralSpace[y].GetLength(0); i++)
                temp[i + NeuralSpace[x].GetLength(0)] = NeuralSpace[y][i];
            NeuralSpace[x] = temp;
            NeuralSpace[y] = null;
        }

         
    

        public static double[,] InformationSimilarityMatrix(bool[][][] NeuralSpace, int ResponseOnSet, int ResponseOffSet, bool DoingMaxInfoClustering/*replace nulls with verysmall or very large values*/)
        {
            double[,] r = new double[NeuralSpace.GetLength(0), NeuralSpace.GetLength(0)];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                for (int j = i+1; j < NeuralSpace.GetLength(0); j++)
                {
                    if ((NeuralSpace[i] == null) || (NeuralSpace[j] == null))
                        r[i, j] = (DoingMaxInfoClustering?double.MinValue:double.MaxValue);
                    else
                    {
                        bool[][][] tempSpace = new bool[2][][] { NeuralSpace[i], NeuralSpace[j] };
                        r[i, j] = Information.MutualInformation(tempSpace, ResponseOnSet, ResponseOffSet);
                    }
                }
            return r;
        }

        /// <summary>
        /// It gets the Neural Space and returns and N by N array ( N dim space) as an Array list that in each cell has a 
        /// </summary>
        /// <param name="NeuralSpace"></param>
        /// <param name="ResponseOnSet"></param>
        /// <param name="ResponseOffSet"></param>
        /// <param name="Dimensions"></param>
        /// <param name="DoingMaxInfoClustering"></param>
        /// <returns></returns>
        public static Array InformationSimilaritySpace(bool[][][] NeuralSpace, int ResponseOnSet, int ResponseOffSet, int Dimensions , bool DoingMaxInfoClustering/*replace nulls with verysmall or very large values*/)
        {
            int[] dims = new int[Dimensions];
            for (int i = 0; i < Dimensions; i++)
                dims[i]=NeuralSpace.GetLength(0);
            Array r = Array.CreateInstance(typeof(double) ,dims);
            long[] indices = new long[Dimensions];
            long d = (int)Math.Pow(NeuralSpace.GetLength(0), Dimensions);
            for (long i = 0; i < d; i++)
            {
                indices = ToNbyNIndices(i,Dimensions, NeuralSpace.GetLength(0));
                bool havenull = false;
                for (int l = 0; l < Dimensions; l++) if (NeuralSpace[l] == null) havenull = true;
                if (havenull) r.SetValue((DoingMaxInfoClustering ? double.MinValue : double.MaxValue), indices);
                else
                {
                    bool[][][] tempSpace = new bool[Dimensions][][];
                    for (int m = 0; m < Dimensions; m++)
                        tempSpace[m] = NeuralSpace[indices[m]];
                    r.SetValue(Information.MutualInformation(tempSpace, ResponseOnSet, ResponseOffSet), indices);
                }
            }
            return r;
        }

        public static long[] ToNbyNIndices(long i, int DimCount, int DimLength)
        {
            long[] r = new long[DimCount];
            int p = DimCount;
            while(p>0)
            {
                if (i >= Math.Pow(DimLength, p))
                {
                    r[p] = (long)Math.Floor(i / Math.Pow(DimLength, p));
                    i -= (long)Math.Pow(DimLength, p)*r[p];
                }
                p--;
            }
            Math.DivRem(i, DimLength, out r[0]);
            return r;
        }


        public static double[,] MaxInfoClusterTree(bool[][][] NeuralSpace, int ResponseOnSet, int ResponseOffSet)
        {
            double[,] tree = new double[NeuralSpace.GetLength(0), 3];
            double[,] SimilarityMatrix = new double[NeuralSpace.GetLength(0), NeuralSpace.GetLength(0)];
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
            {
                SimilarityMatrix = InformationSimilarityMatrix(NeuralSpace, ResponseOnSet, ResponseOffSet, true);

                //find the nearest nighbours
                int x = -1;
                int y = -1;
                double max = double.MinValue;
                for (int j = 0; j < SimilarityMatrix.GetLength(0); j++)
                    for (int k = j + 1; k < SimilarityMatrix.GetLength(1); k++)
                        if (SimilarityMatrix[j, k] > max)
                        {
                            x = j;
                            y = k;
                            max = SimilarityMatrix[j, k];
                        }

                MergeNighbors(ref NeuralSpace, x, y);


                tree[i, 0] = x;
                tree[i, 1] = y;
                tree[i, 2] = max;
            }
            return tree;
        }

        public static Array GroupsofKMinInfoClustering(bool[][][] NeuralSpace, int ResponseOnset, int ResponseOffset, int GroupofKMembers)
        {
            int[] indices = new int[GroupofKMembers];
            for (int i = 0; i < GroupofKMembers; i++) indices[i] = NeuralSpace.GetLength(0);
            Array simmat = Array.CreateInstance(typeof(double), indices);
            simmat = InformationSimilaritySpace(NeuralSpace, ResponseOnset, ResponseOffset,GroupofKMembers ,false);

            double[][] r = new double[321][];
            bool Satisfied = false;
            int x=0, y=0, z=0 ,c = 0;
            while (!Satisfied)
            {
                double tempmin = double.MaxValue;
                for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                {
                    for (int j = 0; j < NeuralSpace.GetLength(0); j++)
                    {
                        for (int k = 0; k < NeuralSpace.GetLength(0); k++)
                        {
                            if (Convert.ToDouble(simmat.GetValue(i, j, k)) < tempmin)
                            {
                                tempmin = Convert.ToDouble(simmat.GetValue(i, j, k));
                                x = i; y = j; z = k;
                            }
                        }
                    }
                }
                
                r[c] = new double[3] { x, y, z };
                simmat.SetValue(double.MaxValue, x, y, z);
                c++;
                if ((x + y + z == 0) && (c > 110)) Satisfied = true;
            }
            return r;
        }

        public static double dPrime(double[] P1, double[] P2)
        {
            //As Afraz, Kiani, Esteky, Nature calculated it d' = (meanface-mean nonface)/ sqrt((meanf+meannf)/2) 
            double r = 0;
            r = (Mean(P1) - Mean(P2)) / Math.Sqrt( (Variance(P1)+Variance(P2)) / 2.0d);
            return r;
        }

        public static int SpikeCount(bool[] SpikeTrain, int Begin, int End)
        {
            int spikecount = 0;
            for (int i = 0; i < SpikeTrain.GetLength(0); i++)
			{
                if (SpikeTrain[i]) spikecount++;
			}
            return spikecount;
        }

        public static double[] CalculateBetweenCategoryVariance(bool[][][] NeuralSpace, int level, int binsize, int TrialDuration)
        {
            double[][,] CR = SpikeTrain.CalculatePSTH(NeuralSpace, level, binsize, TrialDuration, false,false,false);
            double[] tBCV = new double[CR[0].GetLength(0)];

            for (int i = 0; i < CR[0].GetLength(0); i++ )
            {
                double[] temp = new double[CR.GetLength(0) - 2];// "-2" to exlude both average and blank
                for (int j = 0; j < CR.GetLength(0)-2; j++)
                {
                    temp[j] = CR[j][i, 0];
                }
                tBCV[i] = StatisticalTests.Variance(temp);
            }
            return tBCV;
        }

        public static double[] CalculateWithinCategoryVariance(bool[][][] NeuralSpace, int level, int binsize, int TrialDuration)
        {
            double[][,] CR = SpikeTrain.CalculatePSTH(NeuralSpace, level, binsize, TrialDuration, false,false,false);
            double[] tWCV = new double[CR[0].GetLength(0)];
            for (int i = 0; i < CR[0].GetLength(0); i++)
            {
                double[] temp = new double[CR.GetLength(0) - 2];// "-2" to exlude both average and blank
                for (int j = 0; j < CR.GetLength(0) - 2; j++)
                {
                    temp[j] = CR[j][i, 1];
                }
                tWCV[i] = StatisticalTests.Mean(temp);
            }
            return tWCV;
        }

        public static double[] CalculateBetweenonWithinCategoryVariance(bool[][][] NeuralSpace, int level, int binsize, int trialduration)
        {
            double[][,] CR = SpikeTrain.CalculatePSTH(NeuralSpace, level, binsize, trialduration, false, false,false);
            double[] tBCV = new double[CR[0].GetLength(0)];
            double[] tWCV = new double[CR[0].GetLength(0)];
            double[] tWB = new double[CR[0].GetLength(0)];
            for (int i = 0; i < CR[0].GetLength(0); i++)
            {
                double[,] temp = new double[CR.GetLength(0) - 2 , 2];
                for (int j = 0; j < CR.GetLength(0) - 2; j++) // "-2" to exclude both average and blank
                {
                    temp[j, 0] = CR[j][i, 0];
                    temp[j, 1] = CR[j][i, 1];
                }
                tBCV[i] = StatisticalTests.Variance(temp, 0);
                tWCV[i] = StatisticalTests.Mean(temp, 1);
            }
            for (int i = 0; i < tWB.GetLength(0); i++)
            {
                tWB[i] = tBCV[i] / tWCV[i];
            }
            return tWB;
        }


        /// <summary>
        /// Kolmogorov-Smirnov probability function.
        /// </summary>
        /// <param name="alam"></param>
        /// <returns></returns>
        public static double probks(double alam)
        {
            double EPS1 = 0.001d;
            double EPS2 = 1.0e-8;
            int j;
            double a2,fac=2.0f,sum=0.0f,term,termbf=0.0f;
            a2 = -2.0f*alam*alam;
            for (j=1;j<=100;j++) 
            {
                term=fac*Math.Exp(a2*j*j);
                sum += term;
                if (Math.Abs(term) <= EPS1*termbf || Math.Abs(term) <= EPS2*sum) return sum;
                fac = -fac; //Alternating signs in sum.
                termbf=Math.Abs(term);
            }
            return 1.0; //Get here only by failing to converge.
        }

        /// <summary>
        /// Kobatake, Wang, Tanaka JN 1998: The statistical significance of the responses was determined by comparing the mean firing rates within the window for 10 individual responses, with 10 spontaneous firing rates immediately preceding each stimulus presentation, using the Kolmogorov-Smirnov (K-S) test.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Alpha">The confidence level: 0.05 or 0.001</param>
        /// <param name="Tail">Two taild?</param>
        /// <returns></returns>
        public static double KStest2(double[] x, double[] y, double Alpha, StatisticalTestTail Tail)
        {
            double pValue = 0;
            ulong j1=1,j2=1;
            double d1,d2,dt,en1,en2,en,fn1=0.0,fn2=0.0;
            Array.Sort(x);
            Array.Sort(y);
            en1=x.GetLength(0);
            en2=y.GetLength(0);
            double d=0.0d;
            while (j1 < en1 && j2 < en2) //If we are not done...
            { 
                if ((d1=x[j1]) <= (d2=y[j2])) 
                    fn1=j1++/en1; //Next step is in data1.
                if (d2 <= d1) 
                    fn2=j2++/en2; //Next step is in data2.
                if ((dt=Math.Abs(fn2-fn1)) > d) 
                    d=dt;
            }
            en=Math.Sqrt(en1*en2/(en1+en2));
            pValue = probks((en+0.12+0.11/en)*(d)); //Compute significance.
            
            return pValue;
        }




        public static double[] GaussianWindowACausal(int Length, double ratio)
        {
            int N = Length - 1;
            double[] gwin = new double[N];
            return gwin;
        }

        public static double[] GaussianWindow(int Length)
        {
            //ALPHA is defined as the reciprocal of the standard
            //deviation and is a measure of the width of its Fourier Transform.
            //As ALPHA increases, the width of the window will decrease. If omitted,
            //ALPHA is 2.5.
            double alpha = 2.5;
            int N = Length-1;
            double[] gwin = new double[N];
            for (int i = 0; i < N; i++)
			{
                gwin[i] = (i - N/2.0d);
                gwin[i] = Math.Exp(-0.5d*Math.Pow((alpha*gwin[i]/(N/2.0d)),2));
			}
            return gwin;
        }

        public static double[] Convolution(bool[] SpikeTrain, double[] Kernel)
        {
            double[] s = new double[SpikeTrain.GetLength(0)];
            for (int i = 0; i < SpikeTrain.GetLength(0); i++)
                s[i] = Convert.ToInt32(SpikeTrain[i]);
            return Convolution(s, Kernel);
        }
        public static double[] Convolution(int[] Signal, double[] Kernel)
        {
            double[] s = new double[Signal.GetLength(0)];
            for(int i = 0; i<Signal.GetLength(0); i++)
                s[i] = Signal[i];
            return Convolution(s, Kernel);
        }
        public static double[] Convolution(double[] Signal, double[] Kernel)
        {
            double[] r = new double[Signal.GetLength(0)+Kernel.GetLength(0)-1];
            //Padding
            double[] PaddedSignal = new double[Signal.GetLength(0) + (2 * Kernel.GetLength(0))];
            for (int i = 0; i < Signal.GetLength(0); i++)
                PaddedSignal[i+Kernel.GetLength(0)] = Signal[i];
            
            for (int j = 0; j < r.GetLength(0); j++)
                for (int k = 0; k < Kernel.GetLength(0); k++)
                {
                    r[j] += (PaddedSignal[Kernel.GetLength(0) + j - k]) * Kernel[k];
                }
            return r;
        }

        public static double[] AutoCorrelation(bool[] SpikeTrain)
        {
            double[] a = new double[SpikeTrain.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
                if (SpikeTrain[i]) a[i] = 1;
            return AutoCorrelation(a);
        }

        public static double[] AutoCorrelation(double[] a)
        {
            return CrossCorrelation(a, a);
        }

        public static double[] CrossCorrelation(double[] a, double[] b)
        {
            //double[] r = new double[Math.Max(a.GetLength(0), b.GetLength(0))];
            //double[] rb = Reverse(b);
            //r = Convolution(a,rb);

            double[] r = new double[a.GetLength(0) + b.GetLength(0) - 1];
            //Padding
            double[] PaddedSignal = new double[a.GetLength(0) + (2 * b.GetLength(0))];
            for (int i = 0; i < a.GetLength(0); i++)
                PaddedSignal[i + b.GetLength(0)] = a[i];

            for (int j = 0; j < r.GetLength(0); j++)
                for (int k = 0; k < b.GetLength(0); k++)
                {
                    r[j] += (PaddedSignal[j + k]) * b[k];
                }
            return r;

  
        }

        public static double[] Reverse(double[] b)
        {
            int l = b.GetLength(0);
            double[] r = new double[l]; 
            for (int i = 0; i < l; i++)
            {
                r[i]=b[l-i-1];
            }
            return r;
        }

        //public static double Correlation(double[,] sdata)
        //{
        //    double[] x = new double[sdata.GetLength(0)];
        //    double[] y = new double[sdata.GetLength(0)];
        //    for (int i = 0; i < sdata.GetLength(0); i++)
        //    {
        //        x[i] = sdata[i, 0];
        //        y[i] = sdata[i, 1];
        //    }
        //    return Correlation(x, y);
        //}

        //public static double Correlation(double[] x, double[] y)
        //{
        //    double correlation;
        //    double sum_sq_x = 0;
        //    double sum_sq_y = 0;
        //    double sum_coproduct = 0;
        //    double mean_x = x[0];
        //    double mean_y = y[0];
        //    for(int i=1; i< x.GetLength(0); i++)
        //    {
        //        double sweep = (i - 1.0) / i;
        //        double delta_x = x[i] - mean_x;
        //        double delta_y = y[i] - mean_y;
        //        sum_sq_x += delta_x * delta_x * sweep;
        //        sum_sq_y += delta_y * delta_y * sweep;
        //        sum_coproduct += delta_x * delta_y * sweep;
        //        mean_x += delta_x / i;
        //        mean_y += delta_y / i ;
        //    }
        //    double pop_sd_x = Math.Sqrt( sum_sq_x / (double)x.GetLength(0) );
        //    double pop_sd_y = Math.Sqrt( sum_sq_y / (double)y.GetLength(0) );
        //    double cov_x_y = sum_coproduct / (double)x.GetLength(0);
        //    correlation = cov_x_y / (double)(pop_sd_x * pop_sd_y);
        //    return correlation;
        //}

        public static double[,] CorrelationPearsons(double[][] sdata)
        {
            double[,] r = new double[sdata.Length, sdata.Length];
            for (int i = 0; i < sdata.Length; i++)
                for (int j = i; j < sdata.Length; j++)
                    if (i == j)
                        r[i, j] = 1;
                    else
                    {
                        if ((sdata[i] == null) | (sdata[j] == null) | (Mean(sdata[i])==0) | (Mean(sdata[j])==0))
                        {
                            r[i, j] = -1;
                            r[j, i] = -1;
                        }
                        else
                        {
                            //double[] randp = CorrelationPearsons(zscore(sdata[i]), zscore(sdata[j]));
                            double[] randp = CorrelationPearsons(sdata[i], sdata[j]);
                            r[i, j] = randp[0];
                            r[j, i] = randp[0];
                        }
                    }
            return r;
        }

        public static double[] zscore(double[] p)
        {
            double [] r = new double [p.Length];
            double m = Mean(p);
            double s = Math.Sqrt(Variance(p));
            for (int i = 0; i < p.Length; i++)
                r[i] = (p[i]-m)/s;
            return r;
        }

        public static double[][] zscore(double[][] p)
        {
            double[][] r = new double[p.Length][];
            for (int i = 0; i < p.Length; i++)
                if (p[i] != null)
                {
                    r[i] = new double[p[i].Length];
                    double m = Mean(p[i]);
                    double s = Math.Sqrt(Variance(p[i]));
                    for (int j = 0; j < p[i].Length; j++)
                        r[i][j] = (p[i][j] - m) / s;
                }
            return r;
        }

        public static double[][] Transpose(double[][] p)
        {
            double [][] r = new double[p[0].Length][];
            for (int i = 0; i < p[0].Length; i++)
            {
                r[i] = new double[p.Length];
                for (int j = 0; j < p.Length; j++)
                    if(p[j]!=null)
                        r[i][j] = p[j][i];
            }       
            return r;
        }

        public static double[] CorrelationPearsons(double[,] sdata)
        {
            double[] x = new double[sdata.GetLength(0)];
            double[] y = new double[sdata.GetLength(0)];
            for (int i = 0; i < sdata.GetLength(0); i++)
            {
                x[i] = sdata[i, 0];
                y[i] = sdata[i, 1];
            }
            return CorrelationPearsons(x, y);
        }

        public static double[] CorrelationPearsons(double[] x, double[] y)
        {
            //Given two arrays x[1..n] and y[1..n], this routine computes their correlation coefficient
            //r (returned as r), the significance level at which the null hypothesis of zero correlation is
            //disproved (prob whose small value indicates a significant correlation), and Fisher’s z (returned
            //as z), whose value can be used in further statistical tests as described above.

            //float betai(float a, float b, float x);
            //float erfcc(float x);
            double TINY = 1.0e-20;
            double z = 0;
            double r = 0;
            double pValue = 1;
            double yt,xt,t,df;
            double syy=0.0,sxy=0.0,sxx=0.0,ay=0.0,ax=0.0;
            ax = Mean(x);
            ay = Mean(y);
            //Compute the correlation coefficient.
            for (int j=0;j<x.GetLength(0);j++) 
            { 
                xt=x[j]-ax;
                yt=y[j]-ay;
                sxx += xt*xt;
                syy += yt*yt;
                sxy += xt*yt;
            }
            r= sxy /(Math.Sqrt(sxx*syy)+TINY);
            z=0.5* Math.Log((1.0+(r)+TINY)/(1.0-(r)+TINY)); //Fisher’s z transformation.
            df=x.GetLength(0)-2;
            t=(r)*Math.Sqrt(df/((1.0-(r)+TINY)*(1.0+(r)+TINY))); 
            pValue= IncompleteBeta(0.5*df,0.5,df/(df+t*t)); //Student’s t probability.
            /* *prob=erfcc(fabs((*z)*sqrt(n-1.0))/1.4142136) */
            return new double[] { r, pValue };
        }

        public static double[] SubArray(double[] a, int Offset, int Length)
        {
            double[] r = new double[Length];
            for (int i = 0; i < Length; i++)
            {
                r[i] = a[Offset + i];
            }
            return r;
        }

        public static double[,] SubArray(double[,] a, int Offset, int Length)
        {
            double[,] r = new double[Length, a.GetLength(1)];
            for (int i = 0; i < Length; i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    r[i, j] = a[Offset + i, j];
                }
            return r;
        }

        public static bool[][][] SubArray(bool[][][] NeuralSpace, int x, int y, int z)
        {
            // Nuilt but not used
            if (x == 0) x = NeuralSpace.GetLength(0);
            if (y == 0) y = NeuralSpace.GetLength(1);
            if (z == 0) z = NeuralSpace.GetLength(2);

            bool[][][] r = new bool[x][][];
            for (int i = 0; i < y; i++)
            {
                r[i] = new bool[y][];
                for (int j = 0; j < y; j++) 
                    r[i][j] = new bool[z];
            }


            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                    {
                        r[x][y][z] = NeuralSpace[x][y][z];
                    } 
            return r;
        }

        public static double SpikeCountRate(bool[] trial, int start, int end)
        {
            double spr = 0;
            for (int i = start; i < end; i++)
            {
                if (trial[i]) spr++;
            }
            return (1000.0d*spr/(double)(end - start));
        }

        public static double FiringRate(bool[][] Trials, int start, int end)
        {
            double fr = 0.0d;
            for (int i = 0; i < Trials.GetLength(0); i++)
            {
                fr += SpikeCountRate(Trials[i], start, end);
            }
            fr = fr / (double)Trials.GetLength(0);
            return fr;
        }
        public static double BaselineFiringRate(bool[][][] NeuralSpace)
        {
            double Baseline = 0.0d;
            int c = 0;
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                {
                    c++;
                    Baseline += FiringRate(NeuralSpace[i], 0, 100);
                }
            Baseline = Baseline / (double)c;
            return Baseline;
        }

        public static double SparsenessIndex(bool[][][] NeuralSpace, int ResponseOnsetLatency, int ResponseOffsetLatency)
        {
            // Rufin Vogels, European Journal of Neuroscience 11 1239-1255 (1999) - refering to Treves, Rolls 1991
            // Sparsenes = (sigma ( Ri/n))^2 / (sigma (Ri^2 /n))
            // where R is the net response to stimuli for which negative values are cliped to zero.
            double spIndex = 1.0d;
            double[] NetResponses = new double[0];
            double Baseline = BaselineFiringRate(NeuralSpace);
            int c = 0;
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
            if(NeuralSpace[i]!=null)
            if(Config.MapCategory(i)!=20)// Exclude blank
            {
                Array.Resize(ref NetResponses, c + 1);
                NetResponses[c] = Math.Max(0, FiringRate(NeuralSpace[i], ResponseOnsetLatency, ResponseOffsetLatency)-Baseline);
                c++;
            }

            double no=0;
            double deno=0;
            for (int i = 0; i < NetResponses.GetLength(0); i++)
            {
                no += NetResponses[i] / (double)NetResponses.GetLength(0);
                deno += Math.Pow(NetResponses[i],2) / (double)NetResponses.GetLength(0);
            }
            spIndex = Math.Pow(no,2) / deno;
            return spIndex;
        }


        public static double SparsenessIndexCategorical(bool[][][] NeuralSpace, int CatLevel, int ResponseOnsetLatency, int ResponseOffsetLatency)
        {
            // Modified from Rufin Vogels, European Journal of Neuroscience 11 1239-1255 (1999) - refering to Treves, Rolls 1991 to fit Categorical perspective
            // Sparsenes = (sigma ( Ri/n))^2 / (sigma (Ri^2 /n))  where R is the net response to super categories for which negative values are cliped to zero.
            if ((CatLevel != 3) & (CatLevel != 2)) { throw new Exception("Only Catlevel = 2 or 3 are supported"); return 1.0d; }
            double spIndex = 1.0d; 
            double Baseline = BaselineFiringRate(NeuralSpace);
            int c = 0;
            double[,] NetResponses = CalculateResponseMagnitudes(NeuralSpace, CatLevel, ResponseOnsetLatency, ResponseOffsetLatency);
            double no = 0;
            double deno = 0;
            for (int i = 0; i < NetResponses.GetLength(0); i++)
                if (((i != 20) && (CatLevel == 3)) || ((i != 10) && (CatLevel == 2)))// Exclude blank
                {
                    double d = Math.Max(0, NetResponses[i, 0]);
                    no +=  d / (double)NetResponses.GetLength(0);
                    deno += Math.Pow(d, 2) / (double)NetResponses.GetLength(0);
                }
            spIndex = Math.Pow(no, 2) / deno;
            return spIndex;
        }

        /// <summary>
        /// Calcualtes the Categorical Response Magnitudes from a NeuralSpcae Array. Returns an array containg 0: the mean of responses, 1: the Standard error of mean
        /// </summary>
        /// <param name="NeuralSpace">Stimulus - Trial - Spike in Time bin</param>
        /// <param name="level">3: Categorical, 2 Super Categorical, 1 Face non-face, 4 Faces and simples, 5 Supercategories exclusing hands</param>
        /// <param name="ResponseOnsetLatency"></param>
        /// <param name="ResponseOffsetLatency"></param>
        /// <returns></returns>
        public static double[,] CalculateResponseMagnitudes(bool[][][] NeuralSpace, int level, int ResponseOnsetLatency, int ResponseOffsetLatency)
        {
            double[,] ResponseMagnitudes = new double[0, 2];
            int[,] SpikeCounts = new int[NeuralSpace.GetLength(0), 2]; // [Spikes, Miliseconds]
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)  // Stimulus Counter
            {
                if (NeuralSpace[i] != null)
                {
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)// Repetitions
                        for (int k = ResponseOnsetLatency; k < ResponseOffsetLatency; k++) // Time
                        {
                            SpikeCounts[i, 1]++;
                            if (NeuralSpace[i][j][k]) SpikeCounts[i, 0]++;
                        }
                }
            }


            int BarCount = 0;
            switch (level)
            {
                case 1:
                    BarCount = 3;
                    ResponseMagnitudes = new double[BarCount, 2];
                    double[][] tempSE = new double[BarCount][];
                    for (int i = 0; i < SpikeCounts.GetLength(0); i++)
                        if (SpikeCounts[i, 0] != null)
                            if (Config.MapFaceNonFace(i) >= 0)
                            {
                                ResponseMagnitudes[Config.MapFaceNonFace(i), 0] += (double)SpikeCounts[i, 0];
                                ResponseMagnitudes[Config.MapFaceNonFace(i), 1] += (double)SpikeCounts[i, 1];
                                if (tempSE[Config.MapFaceNonFace(i)] == null) tempSE[Config.MapFaceNonFace(i)] = new double[0];
                                Array.Resize(ref tempSE[Config.MapFaceNonFace(i)], tempSE[Config.MapFaceNonFace(i)].GetLength(0) + 1);
                                tempSE[Config.MapFaceNonFace(i)][tempSE[Config.MapFaceNonFace(i)].GetLength(0) - 1] = (double)SpikeCounts[i, 0];
                            }
                    for (int i = 0; i < BarCount; i++)
                        if (tempSE[i] != null)
                        {
                            ResponseMagnitudes[i, 0] = StatisticalTests.Mean(tempSE[i]);
                            ResponseMagnitudes[i, 1] = Math.Sqrt(StatisticalTests.Variance(tempSE[i])) / (double)Math.Sqrt((tempSE[i].GetLength(0)));
                        }

                    break;
                case 2:
                    BarCount = 11;
                    ResponseMagnitudes = new double[BarCount, 2];
                    tempSE = new double[BarCount][];
                    // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
                    for (int i = 0; i < SpikeCounts.GetLength(0); i++)
                        if (SpikeCounts[i, 0] != null)
                            if (Config.MapSuperCategory(i) >= 0)
                            {
                                ResponseMagnitudes[Config.MapSuperCategory(i), 0] += (double)SpikeCounts[i, 0];
                                ResponseMagnitudes[Config.MapSuperCategory(i), 1] += (double)SpikeCounts[i, 1];
                                if (tempSE[Config.MapSuperCategory(i)] == null) tempSE[Config.MapSuperCategory(i)] = new double[0];
                                Array.Resize(ref tempSE[Config.MapSuperCategory(i)], tempSE[Config.MapSuperCategory(i)].GetLength(0) + 1);
                                tempSE[Config.MapSuperCategory(i)][tempSE[Config.MapSuperCategory(i)].GetLength(0) - 1] = (double)SpikeCounts[i, 0];
                                //tempSE[Config.MapSuperCategory(i)] += Math.Pow(SpikeCounts[i, 0], 2);
                            }
                    for (int i = 0; i < BarCount; i++)
                        if (tempSE[i] != null)
                        {
                            ResponseMagnitudes[i, 0] = StatisticalTests.Mean(tempSE[i]);
                            ResponseMagnitudes[i, 1] = Math.Sqrt(StatisticalTests.Variance(tempSE[i])) / (double)Math.Sqrt((tempSE[i].GetLength(0)));
                        }

                    break;
                case 3:
                    BarCount = 21;
                    ResponseMagnitudes = new double[BarCount, 2];
                    tempSE = new double[BarCount][];
                    // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
                    for (int i = 0; i < SpikeCounts.GetLength(0); i++)
                        if (SpikeCounts[i, 0] != null)
                            if (Config.MapCategory(i) >= 0)
                            {
                                ResponseMagnitudes[Config.MapCategory(i), 0] += (double)SpikeCounts[i, 0];
                                ResponseMagnitudes[Config.MapCategory(i), 1] += (double)SpikeCounts[i, 1];
                                if (tempSE[Config.MapCategory(i)] == null) tempSE[Config.MapCategory(i)] = new double[0];
                                Array.Resize(ref tempSE[Config.MapCategory(i)], tempSE[Config.MapCategory(i)].GetLength(0) + 1);
                                tempSE[Config.MapCategory(i)][tempSE[Config.MapCategory(i)].GetLength(0) - 1] = (double)SpikeCounts[i, 0];
                                //tempSE[Config.MapCategory(i)] += Math.Pow(SpikeCounts[i, 0], 2);
                            }
                    for (int i = 0; i < BarCount; i++)
                        if (tempSE[i] != null)
                        {
                            ResponseMagnitudes[i, 0] = StatisticalTests.Mean(tempSE[i]);
                            ResponseMagnitudes[i, 1] = Math.Sqrt(StatisticalTests.Variance(tempSE[i])) / (double)Math.Sqrt((tempSE[i].GetLength(0)));
                        }

                    break;
                case 4:
                    BarCount = 8;
                    ResponseMagnitudes = new double[BarCount, 2];
                    tempSE = new double[BarCount][];
                    tempSE = new double[BarCount][];
                    // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
                    for (int i = 0; i < SpikeCounts.GetLength(0); i++)
                        if (SpikeCounts[i, 0] != null)
                            if (Config.MapSuperCategory2(i) >= 0)
                            {
                                ResponseMagnitudes[Config.MapSuperCategory2(i), 0] += (double)SpikeCounts[i, 0];
                                ResponseMagnitudes[Config.MapSuperCategory2(i), 1] += (double)SpikeCounts[i, 1];
                                if (tempSE[Config.MapSuperCategory2(i)] == null) tempSE[Config.MapSuperCategory2(i)] = new double[0];
                                Array.Resize(ref tempSE[Config.MapSuperCategory2(i)], tempSE[Config.MapSuperCategory2(i)].GetLength(0) + 1);
                                tempSE[Config.MapSuperCategory2(i)][tempSE[Config.MapSuperCategory2(i)].GetLength(0) - 1] = (double)SpikeCounts[i, 0];
                            }
                    for (int i = 0; i < BarCount; i++)
                        if (tempSE[i] != null)
                        {
                            ResponseMagnitudes[i, 0] = StatisticalTests.Mean(tempSE[i]);
                            ResponseMagnitudes[i, 1] = Math.Sqrt(StatisticalTests.Variance(tempSE[i])) / (double)Math.Sqrt((tempSE[i].GetLength(0)));
                        }
                    break;
                case 5:
                    BarCount = 8;
                    ResponseMagnitudes = new double[BarCount, 2];
                    tempSE = new double[BarCount][];
                    // the other method : ( sigma(x2) - ( sigma(x)2 / N) ) / (N-1)
                    for (int i = 0; i < SpikeCounts.GetLength(0); i++)
                        if (SpikeCounts[i, 0] != null)
                            if (Config.MapSuperCategory3(i) >= 0)
                            {
                                ResponseMagnitudes[Config.MapSuperCategory3(i), 0] += (double)SpikeCounts[i, 0];
                                ResponseMagnitudes[Config.MapSuperCategory3(i), 1] += (double)SpikeCounts[i, 1];
                                if (tempSE[Config.MapSuperCategory3(i)] == null) tempSE[Config.MapSuperCategory3(i)] = new double[0];
                                Array.Resize(ref tempSE[Config.MapSuperCategory3(i)], tempSE[Config.MapSuperCategory3(i)].GetLength(0) + 1);
                                tempSE[Config.MapSuperCategory3(i)][tempSE[Config.MapSuperCategory3(i)].GetLength(0) - 1] = (double)SpikeCounts[i, 0];
                            }
                    for (int i = 0; i < BarCount; i++)
                        if (tempSE[i] != null)
                        {
                            ResponseMagnitudes[i, 0] = StatisticalTests.Mean(tempSE[i]);
                            ResponseMagnitudes[i, 1] = Math.Sqrt(StatisticalTests.Variance(tempSE[i])) / (double)Math.Sqrt((tempSE[i].GetLength(0) - 1.0d));
                        }
                    break;
            }
            for (int i = 0; i < ResponseMagnitudes.GetLength(0); i++)
            {
                if (Double.IsNaN(ResponseMagnitudes[i, 0])) ResponseMagnitudes[i, 0] = 0.0d;
                if (Double.IsNaN(ResponseMagnitudes[i, 1])) ResponseMagnitudes[i, 1] = 0.0d;
            }
            return ResponseMagnitudes;
        }


        public static double[,] AutoCorrelogram(bool[][][] NeuralSpace)
        {
            double[,] r = new double[NeuralSpace[0][0].GetLength(0)*2,2];

            double[][] temp = new double[NeuralSpace.GetLength(0) * NeuralSpace[0].GetLength(0)][];
            int c = 0;
            for (int i = 0; i < NeuralSpace.GetLength(0); i++)
                if (NeuralSpace[i] != null)
                    for (int j = 0; j < NeuralSpace[i].GetLength(0); j++)
                    {
                        temp[c] = AutoCorrelation(NeuralSpace[i][j]);
                        c++;
                    }
            double[] t = Mean(temp);
            for (int k = 0; k < t.GetLength(0); k++)
                r[k, 0] = t[k];
            return r;
        }

        public static double[,] Frequencies(double[] a)
        {
            double[,] r = new double[1, 1];
            return r;
        }

        public static double[] CalculateBetweenMinusWithinCategoryVariance(bool[][][] NeuralSpace, int level, int binsize, int trialduration)
        {
        
            double[][,] CR = SpikeTrain.CalculatePSTH(NeuralSpace, level, binsize, trialduration, false, false, false);
            double[] tBCV = new double[CR[0].GetLength(0)];
            double[] tWCV = new double[CR[0].GetLength(0)];
            double[] tWB = new double[CR[0].GetLength(0)];
            for (int i = 0; i < CR[0].GetLength(0); i++)
            {
                double[,] temp = new double[CR.GetLength(0) - 2, 2];
                for (int j = 0; j < CR.GetLength(0) - 2; j++) // "-2" to exclude both average and blank
                {
                    temp[j, 0] = CR[j][i, 0];
                    temp[j, 1] = CR[j][i, 1];
                }
                tBCV[i] = StatisticalTests.Variance(temp, 0);
                tWCV[i] = StatisticalTests.Mean(temp, 1);
            }
            for (int i = 0; i < tWB.GetLength(0); i++)
            {
                tWB[i] = (tBCV[i] - tWCV[i]) / (tBCV[i] + tWCV[i]);
            }
            return tWB;
        }

    }
}
