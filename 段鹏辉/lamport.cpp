#include <iostream>
#include <stdio.h>
#include <cmath>
#include <string>

using namespace std;

int quickpow(int m, int n, int k)
{
    int  b = 1;
    while (n > 0)
    {
        if (n & 1)
            b = (b * m) % k;
        n = n >> 1;
        m = (m * m) % k;
    }
    return b;
}

int main()
{
    int n, MOD;
    cin >> n >> MOD;

    int y10, y11, y20, y21, y30, y31;
    cin >> y10 >> y11 >> y20 >> y21 >> y30 >> y31;

    int z10, z11, z20, z21, z30, z31;
    //
    z10 = quickpow(n, y10, MOD);
    z11 = quickpow(n, y11, MOD);
    z20 = quickpow(n, y20, MOD);
    z21 = quickpow(n, y21, MOD);
    z30 = quickpow(n, y30, MOD);
    z31 = quickpow(n, y31, MOD);
    //

    string cl;
    cin >> cl;
    int len = cl.size();
    int Sign[100];

    if (cl[0] == '0')
        Sign[0] = y30;
    else
        Sign[0] = y31;
    if (cl[1] == '0')
        Sign[1] = y20;
    else
        Sign[1] = y21;
    if (cl[2] == '0')
        Sign[2] = y10;
    else
        Sign[2] = y11;


    int Sign_ans[100];

    for (int i = 0; i < 3; i++)
    {
        Sign_ans[i] = quickpow(n, Sign[2 - i], MOD);
        cout << Sign_ans[i] << endl;
    }


    return 0;
}