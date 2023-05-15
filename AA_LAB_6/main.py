from decimal import *
import time
import matplotlib.pyplot as plt

def pi_brent_salamin_digit(n):
    getcontext().prec = n+1
    a = Decimal(1)
    b = Decimal(1) / Decimal(2).sqrt()
    t = Decimal(1) / Decimal(4)
    p = Decimal(1)
    pi_last = Decimal(0)
    while True:
        a_next = (a + b) / 2
        b_next = (a * b).sqrt()
        t_next = t - p * (a - a_next) ** 2
        p_next = 2 * p
        a, b, t, p = a_next, b_next, t_next, p_next
        pi_approx = (a + b) ** 2 / (4 * t)
        if int(pi_approx * 10 ** n) != int(pi_last * 10 ** n):
            return int(str(pi_approx)[n+1])
        pi_last = pi_approx


def pi_bbp_digit(n):
    getcontext().prec = n+1
    pi = Decimal(0)
    for k in range(n+1):
        pi += (Decimal(1) / Decimal(16) ** Decimal(k)) * (
              Decimal(4) / (Decimal(8) * Decimal(k) + Decimal(1)) -
              Decimal(2) / (Decimal(8) * Decimal(k) + Decimal(4)) -
              Decimal(1) / (Decimal(8) * Decimal(k) + Decimal(5)) -
              Decimal(1) / (Decimal(8) * Decimal(k) + Decimal(6)))
    return int(str(pi)[n+1])


def pi_machin_digit(n):
    getcontext().prec = n+1
    pi = Decimal(0)
    for k in range(n+1):
        pi += Decimal((-1)**k) * Decimal(1) / Decimal(2*k+1) * (
              Decimal(4) * Decimal(1) / Decimal(5) ** Decimal(2*k+1) -
              Decimal(1) / Decimal(239) ** Decimal(2*k+1))
    return int(str(pi * 4)[n+1])


def time_function(f, n):
    start = time.time()
    result = f(n)
    end = time.time()
    return end - start, result


def compare_functions(n_values):
    functions = [
        pi_brent_salamin_digit,
        pi_bbp_digit,
        pi_machin_digit,
    ]
    times = [[] for _ in range(len(functions))]
    for n in n_values:
        print(f"n={n}")
        for i, f in enumerate(functions):
            t, result = time_function(f, n)
            times[i].append(t)
            print(f"{f.__name__}: {t:.4f}s, pi_{n}={result}")
        print()
    # plot results
    for i, f in enumerate(functions):
        plt.plot(n_values, times[i], label=f.__name__)
    plt.xlabel('n')
    plt.ylabel('Time (s)')
    plt.legend()
    plt.show()

n_values = [100, 500, 1000, 1500, 2000, 2500]
compare_functions(n_values)
