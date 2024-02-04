using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Final
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashEkleLinked("12345678902");   //  78
            HashEkleLinked("12345678903");
            HashEkleLinked("12345678913");
            HashEkleLinked("12345678920");   //78
            HashEkleLinked("12345678911");

            Console.WriteLine(HashSearchLinked("12345678920"));
            Console.WriteLine(HashSearchLinked("12345678903"));
            Console.WriteLine(HashSearchLinked("12345678913"));
            Console.WriteLine(HashSearchLinked("12345678902"));
            Console.WriteLine(HashSearchLinked("12345678911"));


            HashDeleteLinked("12345678913");
            HashDeleteLinked("12345678920");
            HashDeleteLinked("12345678911");
            Console.WriteLine(HashSearchLinked("12345678913"));
            Console.WriteLine(HashSearchLinked("12345678920"));
            Console.WriteLine(HashSearchLinked("12345678911"));
            Console.ReadLine();
        }
        static string[] hash = new string[100];
        static string[] coll = new string[20]; // 20 tane çakışmaya kadar izin veriliyor
        static int collindis = -1;
        static int hashFunction(string st)
        {
            int indis = 0;
            for (int i = 0; i < st.Length; i++)
            {
                indis = indis + Convert.ToInt32(st[i]);
            }
            indis = indis % hash.Length;
            return indis;

        }
        static void hashDelte(string st)
        {
            int indis = hashFunction(st);
            if (hash[indis] == st)
            {
                hash[indis] = null;
            }
            else
            {
                for (int i = 0; i<  coll.Length; i++)
                {
                    if (coll[i] == st)
                    {
                        coll[i] = null;
                    }
                    else Console.WriteLine("silinecek değer bulunamadı");
                }
            }
                
        }
        static int hashSearch(string st)
        {
            int indis = hashFunction(st);
            if (hash[indis] == st) return 1;
            else
            {
                int bl = 0;
                for (int i = 0; i < coll.Length; i++)
                {
                    if (coll[i] == st)
                    {
                        bl = 1; break;
                    }

                }
                return bl;

            }


        }
        static void haskEkle(string st)
        {
            int indis = hashFunction(st);
            if (hash[indis] != null)
            {
                //collindis++;
                //coll[collindis] = st;
                for (int i = 0; i < coll.Length; i++)
                {
                    if (coll[i] == null) { coll[i] = st; break; }
                }
                
            }
           else hash[indis] = st;
        }


        // linked listlerle olan çözüm


        static Block[] HashLinked = new Block[100];
        static void HashEkleLinked(string st)
        {
            int indis = hashFunction(st);
            if (HashLinked[indis]== null)
            {
                HashLinked[indis] = new Block();
                HashLinked[indis].hashdata = st;
            }
            else
            {
                //1.çözüm
                //HashLinked[indis].prev = new Block();
                //HashLinked[indis].prev.hashdata = st;
                //HashLinked[indis].prev.next = HashLinked[indis];
                //HashLinked[indis] = HashLinked[indis].prev;


                // 2.çözüm sona ekleme
                Block tmp = HashLinked[indis];
                while (tmp.next != null) tmp = tmp.next;
                Block bl = new Block();
                bl.hashdata = st;
                bl.prev = tmp;
                tmp.next = bl;

            }
            
        }
        static int HashSearchLinked(string st)
        {
            int indis = hashFunction(st);
            int bulundu = 0;
            Block tmp = HashLinked[indis];
            while (tmp != null) 
            {
                if(tmp.hashdata == st)
                {
                    bulundu = 1;
                    break;
                }
                tmp = tmp.next;
            }
            return bulundu;
        }
        static void HashDeleteLinked( string st)
        {
            int indis = hashFunction(st);
            if (HashLinked[indis] == null) return;
            if (HashLinked[indis].next == null)
            { HashLinked[indis] = null; return; }
            if (HashLinked[indis].next != null)
            {
                Block tmp = HashLinked[indis];
                while (tmp != null)
                {
                    if (tmp.hashdata == st)
                    {
                        if (tmp.next == null)
                            tmp.prev.next = null;
                        else
                        {
                            tmp.prev.next = tmp.next;
                            tmp.next.prev = tmp.prev;
                        }
                        
                    }
                    tmp = tmp.next;
                }
            }
        }



        static string[] HASH = new string[100];
        static string[] DictData = new string[100];
        static string[] HashNew = new string[20];
        static void hashEkleDict(String key, string data)
        {
            int indis = hashFunction(key);
            if (HASH[indis] == null)
            {
                HASH[indis] = key;
                DictData[indis] = data;
                return;
            }
            else
            {
                for (int i = 0; i < HASH.Length; i++)
                {
                    if (HASH[i] == null)
                    {
                        HASH[i] = key;
                        DictData[i] = data;
                    }
                }
            }

        }
    }
    class Block
    {
        public string hashdata;
        public Block next;
        public Block prev;
    }
}
