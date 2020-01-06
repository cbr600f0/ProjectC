using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Pages
{
    public static class ConfigFile
    {
        //Amount of words already made (change this number to a large number (example: 30) to see the scroll function.)
        //Don't raise this number to high. It'll take a long time to create all the elements (100 word rows might take over 15 seconds to create)
        public static int wordRows = 0;
        //the name speaks for itself. Change this to 15 to create a 15 letter word.
        public static int wordLength;
        // This number is used for the "heightRequest" property. Without this, the element will scale down to it's biggest element which is troublesome for frames
        // Through hight "request", the element will choose between 1: the largest available size (what we want) and 2: this number
        public static int unrealHighNumber = 1000000;

        public static Char[] charPool = "AAAAAABBCCDDDDDEEEEEEEEEEEEEEEEEEFFGGGHHIIIIJJKKKLLLMMMNNNNNNNNNNOOOOOOPPQRRRRRSSSSSTTTTTUUUVVWWXYZZ".ToCharArray();
        public static Random random = new Random(DateTime.Now.Millisecond);
    }
}
