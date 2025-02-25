// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

namespace KMath.Random {
    class Mt19937 {
        public const int N = 624;
        public const int M = 397;

        public const ulong MATRIX_A = 0x9908b0dfUL;   /* constant vector a */
        public const ulong UPPER_MASK = 0x80000000UL; /* most significant w-r bits */
        public const ulong LOWER_MASK = 0x7fffffffUL; /* least significant r bits */
        public static ulong[] mt = new ulong[N]; /* the array for the state vector  */
        public static int mti = N + 1; /* mti==N+1 means mt[N] is not initialized */   

        /* initializes mt[N] with a seed */
        public static void init_genrand(ulong s)
        {
            mt[0]= s & 0xffffffffUL;
            for (mti = 1; mti < N; ++mti) {
                mt[mti] = (1812433253UL * (mt[mti-1] ^ (mt[mti-1] >> 30)) + (ulong)mti);
                /* See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. */
                /* In the previous versions, MSBs of the seed affect   */
                /* only MSBs of the array mt[].                        */
                /* 2002/01/09 modified by Makoto Matsumoto             */
                mt[mti] &= 0xffffffffUL;
                /* for >32 bit machines */
            }
        }
        
        /* initialize by an array with array-length */
        /* init_key is the array for initializing keys */
        /* key_length is its length */
        /* slight change for C++, 2004/2/26 */
        public static void init_by_array(ulong[] init_key, int key_length)
        {
            int i, j, k;
            init_genrand(19650218UL);
            i=1; j=0;
            k = (N > key_length ? N : key_length);
            for (; k != 0; --k) {
                mt[i] = (mt[i] ^ ((mt[i-1] ^ (mt[i-1] >> 30)) * 1664525UL))
                  + init_key[j] + (ulong)j; /* non linear */
                mt[i] &= 0xffffffffUL; /* for WORDSIZE > 32 machines */
                i++; j++;
                if (i >= N) { mt[0] = mt[N-1]; i = 1; }
                if (j >= key_length) j = 0;
            }
            for (k = N - 1; k != 0; --k) {
                mt[i] = (mt[i] ^ ((mt[i-1] ^ (mt[i-1] >> 30)) * 1566083941UL)) - (ulong)i; /* non linear */
                mt[i] &= 0xffffffffUL; /* for WORDSIZE > 32 machines */
                i++;
                if (i >= N) { mt[0] = mt[N-1]; i = 1; }
            }
        
            mt[0] = 0x80000000UL; /* MSB is 1; assuring non-zero initial array */
        }
        
        /* generates a random number on [0,0xffffffff]-interval */
        public static ulong genrand_int32()
        {
            ulong y;
            ulong[] mag01 = new ulong[2]{0x0UL, MATRIX_A};
            /* mag01[x] = x * MATRIX_A  for x=0,1 */
        
            if (mti >= N) { /* generate N words at one time */
                int kk;
        
                if (mti == N + 1)   /* if init_genrand() has not been called, */
                    init_genrand(5489UL); /* a default initial seed is used */
        
                for (kk = 0; kk < N - M; ++kk) {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk+1] & LOWER_MASK);
                    mt[kk] = mt[kk+M] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }
                for (; kk < N-1; ++kk) {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk+1] & LOWER_MASK);
                    mt[kk] = mt[kk+(M-N)] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }
                y = (mt[N-1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[N-1] = mt[M-1] ^ (y >> 1) ^ mag01[y & 0x1UL];
        
                mti = 0;
            }
        
            y = mt[mti++];
        
            /* Tempering */
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680UL;
            y ^= (y << 15) & 0xefc60000UL;
            y ^= (y >> 18);
        
            return y;
        }
        
        public static long genrand_int31()
        {
            return (long)(genrand_int32() >> 1);
        }
        
        /* generates a random number on [0,1]-real-interval */
        public static double genrand_real1()
        {
            return genrand_int32() * (1.0 / 4294967295.0);
            /* divided by 2^32-1 */
        }
        
        public static float genrand_realf()
        {
            return genrand_int32() * (1.0f / 4294967295.0f);
        }
        
        /* generates a random number on [0,1)-real-interval */
        public static double genrand_real2()
        {
            return genrand_int32() * (1.0 / 4294967296.0);
            /* divided by 2^32 */
        }
        
        /* generates a random number on (0,1)-real-interval */
        public static double genrand_real3()
        {
            return (((double)genrand_int32()) + 0.5) * (1.0 / 4294967296.0);
            /* divided by 2^32 */
        }
        
        /* generates a random number on [0,1) with 53-bit resolution*/
        public static double genrand_res53()
        {
            ulong a = genrand_int32() >> 5, b = genrand_int32() >> 6;
            return(a * 67108864.0 + b) * (1.0 / 9007199254740992.0);
        }
        /* Wrappers with better names */
        
        public static float mrandd()
        {
            return (float)genrand_real1();
        }
        
        public static float mrandf()
        {
            return genrand_realf();
        }
        
        public static int mrand()
        {
            return (int)genrand_int32();
        }
        
        public static void seed_twister(ulong s)
        {
            init_genrand(s);
        }
    }
}