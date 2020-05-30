import math
import random


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
        print('input your number P: ', end='')
        p = input()
        p = int(p)
        flag = is_prime(p)
    print(str(p) + ' is a prime ')

    # 得到素数的一个原根
    list = generator(p)
    print(str(p) + ' 的一个原根为：', end='')
    print(list[-1])
    print('------------------------------------------------------------------------------')

    # 得到A的私钥
    XA = random.randint(0, p - 1)
    print('A随机生成的私钥为：%d' % XA)

    # 得到B的私钥
    XB = random.randint(0, p - 1)
    print('B随机生成的私钥为：%d' % XB)
    print('------------------------------------------------------------------------------')

    # 得待A的计算数
    YA = get_calculation(p, int(list[-1]), XA)
    print('A的计算数为：%d' % YA)

    # 得到B的计算数
    YB = get_calculation(p, int(list[-1]), XB)
    print('B的计算数为：%d' % YB)
    print('------------------------------------------------------------------------------')

    #模拟中间人攻击
    print('此时你已经截获了A与B交换的信息，因为p和g是公开的，开始用自己的私钥进行加密')
    XC = random.randint(0, p - 1)
    print('你随机生成的私钥为：%d' % XC)
    #你的计算数
    YC = get_calculation(p, int(list[-1]), XC)
    print('你的计算数为: %d' % YC)
    print('然后你将自己生成的计算数发送给A和B')
    print('------------------------------------------------------------------------------')

    # 交换后A的密钥
    key_A = get_key(XA, YC, p)
    print('A的生成密钥为：%d' % key_A)

    # 交换后B的密钥
    key_B = get_key(XB, YC, p)
    print('B的生成密钥为：%d' % key_B)
    print('------------------------------------------------------------------------------')
    print('此时你就分别获得了和A、B进行通信的加密密钥')
    print('只需要将截获的A的信息用A的生成密钥解密即可')
    print('然后将明文用B的生成密钥加密即可')