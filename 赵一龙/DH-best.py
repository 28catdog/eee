import math
import random

print('首先通信双方在交换协议前预先共享一个密钥q.\n然后使用预先约定的函数从大素数P中生成两个整数Q和Q ^ -1')

def is_prime(p):
    if p <= 1:
        return False
    i = 2
    while i * i <= p:
        if p % i == 0:
            return False
        i += 1
    return True


def generator(p):
    a = 2
    list = []
    while a < p:
        flag = 1
        while flag != p:
            if (a ** flag) % p == 1:
                break
            flag += 1
        if flag == (p - 1):
            list.append(a)
        a += 1
    return list


#求逆元
def EX_GCD(a,b,arr): #扩展欧几里得
    if b == 0:
        arr[0] = 1
        arr[1] = 0
        return a
    g = EX_GCD(b, a % b, arr)
    t = arr[0]
    arr[0] = arr[1]
    arr[1] = t - int(a / b) * arr[1]
    return g


def ModReverse(a,n):
    arr = [0,1,]
    gcd = EX_GCD(a,n,arr)
    if gcd == 1:
        return (arr[0] % n + n) % n
    else:
        return -1

# A，B得到各自的计算数
def get_calculation(p, a, X):
    Y = (a ** X) % p
    return Y


# A，B得到交换计算数后的密钥
def get_key(X, Y, p):
    key = (Y ** X) % p
    return key


if __name__ == "__main__":

    # 得到规定的素数
    flag = False
    while flag == False:
        print('input  number P: ', end='')
        p = input()
        p = int(p)
        flag = is_prime(p)
    print(str(p) + ' is a prime ')

    # 得到素数的一个原根
    list = generator(p)
    print(str(p) + ' 的一个原根为：', end='')
    print(list[-1])
    print('------------------------------------------------------------------------------')

    #生成Q和Q^ -1
    Q = random.randint(0, p - 1)
    RQ = ModReverse(Q , p)
    print('在正式通信中，需要双方预先约定好只会产生一种结果的函数来产生Q和RQ')
    print('Q = %d' % Q)
    print('RQ = %d' % RQ)
    print('------------------------------------------------------------------------------')
    # 得到A的私钥
    x = random.randint(0, p - 1)
    print('A随机生成的私钥为：%d' % x)
    XA = x * Q
    # 得到B的私钥
    y = random.randint(0, p - 1)
    print('B随机生成的私钥为：%d' % y)
    XB = y * Q
    print('------------------------------------------------------------------------------')

    # 得待A的计算数
    YA = get_calculation(p, int(list[-1]), XA)
    print('A的计算数为：%d' % YA)
    YB = get_calculation(p, int(list[-1]), XB)
    print('B的计算数为：%d' % YB)
    print('------------------------------------------------------------------------------')
    print('将得到的公钥发送给对方')
    print('------------------------------------------------------------------------------')

    # 交换后A的密钥
    key_A = get_key(x * RQ, YB, p)
    print('A的生成密钥为：%d' % key_A)

    # 交换后B的密钥
    key_B = get_key(y * RQ, YA, p)
    print('B的生成密钥为：%d' % key_B)
    print('------------------------------------------------------------------------------')
    print('A和B在分别用Q作为指数加密key_A与key_B,并发送给对方')
    input_key_A = get_key(Q , key_B , p)
    input_key_B = get_key(Q , key_A , p)
    print('A收到的结果为 %d' % input_key_B)
    print('B收到的结果为 %d' % input_key_A)
    print('A和B分别用RQ作为指数做运算，看与自己生成的key_A与key_B是否相等')
    KEY_A = get_key(RQ , input_key_B , p)
    KEY_B = get_key(RQ , input_key_A , p)
    print('---------------------------True or False------------------------------------')
    print(KEY_A == KEY_B)
    print('当加密信息杯解惑，由于敌手不知道约定好的Q，导致发送的YC为g ^c ,缺少Q，因此在用RQ进行验证时无法消去Q和RQ，导致身份不可信。')