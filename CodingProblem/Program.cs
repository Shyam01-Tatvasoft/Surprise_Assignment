using System.Numerics;
// 1
// int[] arr = { 1, 2, 3, 5, 6, 7, 8, 9, 10 };
// int x = arr[0];
// for (int i = 1; i < arr.Length; i++)
// {
//     if (arr[i] != x + 1)
//     {
//         Console.WriteLine("Missing element is");
//         Console.WriteLine(x + 1);
//         return;
//     }
//     x = arr[i];
// }


// // 2
// string str = "aaabbccc";
// string ans = "";

// Dictionary<char, int> myHashMap = new Dictionary<char, int>();
// foreach (char c in str)
// {
//     if (myHashMap.ContainsKey(c))
//     {
//         myHashMap[c]++;
//     }
//     else
//     {
//         myHashMap[c] = 1;
//     }
// }

// foreach (var item in myHashMap)
// {
//     ans += item.Key;
//     ans += item.Value;
// }

// Console.WriteLine(ans);


// // 3
// int[] arr = { 2, 7, 11, 15, -2, 4 };
// int target = 9;
// var seen = new HashSet<int>();
// var pairs = new HashSet<(int, int)>();

// foreach (var num in arr)
// {
//     int complement = target - num;
//     if (seen.Contains(complement))
//     {
//         var pair = num < complement ? (num, complement) : (complement, num);
//         pairs.Add(pair);
//     }
//     seen.Add(num);
// }

// List<(int, int)> list = new List<(int, int)>(pairs);

// foreach (var pair in list)
// {
//     Console.WriteLine($"({pair.Item1}, {pair.Item2})");
// }


// //4
// Dictionary<char, int> HashMap4 = new Dictionary<char, int>();
// string str1 = "listen";
// string str2 = "silentt";
// foreach (char c in str1)
// {
//     if (HashMap4.ContainsKey(c))
//     {
//         HashMap4[c]++;
//     }
//     else
//     {
//         HashMap4[c] = 1;
//     }
// }
// bool flag = true;
// foreach (char c in str2)
// {
//     if (HashMap4.ContainsKey(c) && HashMap4[c] > 0)
//     {
//         HashMap4[c]--;
//     }
//     else
//     {
//         flag = false;
//     }
// }
// Console.WriteLine(flag);


// // 5
// string str1 = "aabbcdeff";
// Dictionary<char, int> HashMap2 = new Dictionary<char, int>();

// foreach (char c in str1)
// {
//     if (HashMap2.ContainsKey(c))
//     {
//         HashMap2[c]++;
//     }
//     else
//     {
//         HashMap2[c] = 1;
//     }
// }

// foreach (var item in HashMap2)
// {
//     if (item.Value < 2)
//     {
//         Console.WriteLine(item.Key);
//         return;
//     }
// }

// // 6
// int[] arr = { 3, 3, 4, 2, 3, 3, 3, 1 };

// Dictionary<int, int> HashMap2 = new Dictionary<int, int>();

// foreach (int c in arr)
// {
//     if (HashMap2.ContainsKey(c))
//     {
//         HashMap2[c]++;
//     }
//     else
//     {
//         HashMap2[c] = 1;
//     }
// }

// foreach (var item in HashMap2)
// {
//     if (item.Value > (int)(arr.Length / 2))
//     {
//         Console.WriteLine(item.Key);
//         return;
//     }
// }
