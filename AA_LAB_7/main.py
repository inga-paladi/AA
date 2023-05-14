import time
import random
import heapq
import matplotlib.pyplot as plt


class UnionFind:
    def __init__(self, n):
        self.parent = list(range(n))
        self.rank = [0] * n

    def find(self, i):
        if self.parent[i] != i:
            self.parent[i] = self.find(self.parent[i])
        return self.parent[i]

    def union(self, i, j):
        pi, pj = self.find(i), self.find(j)
        if pi == pj:
            return False
        if self.rank[pi] < self.rank[pj]:
            pi, pj = pj, pi
        self.parent[pj] = pi
        if self.rank[pi] == self.rank[pj]:
            self.rank[pi] += 1
        return True


def prim(n, adj_list):
    visited = [False] * n
    pq = [(0, 0)]
    heapq.heapify(pq)
    mst = []
    while pq:
        w, u = heapq.heappop(pq)
        if visited[u]:
            continue
        visited[u] = True
        mst.append((u, w))
        for v, weight in adj_list[u]:
            if not visited[v]:
                heapq.heappush(pq, (weight, v))
    return mst


def kruskal(n, edges):
    edges.sort(key=lambda e: e[2])
    uf = UnionFind(n)
    mst = []
    for u, v, w in edges:
        if uf.union(u, v):
            mst.append((u, v, w))
        if len(mst) == n - 1:
            break
    return mst


def generate_graph(n):
    adj_list = [[] for _ in range(n)]
    for u in range(n):
        for v in range(u + 1, n):
            weight = random.randint(1, 10)
            adj_list[u].append((v, weight))
            adj_list[v].append((u, weight))
        adj_list[u].sort(key=lambda x: x[1])
    return adj_list


graph_sizes = [100, 500, 1000, 1500, 2000, 2500]
graphs = {}
prim_times = []
kruskal_times = []
for n in graph_sizes:
    adj_list = generate_graph(n)
    graphs[n] = adj_list

    edges = [(u, v, w) for u, neighbors in enumerate(adj_list)
             for v, w in neighbors]

    start = time.time()
    prim(n, adj_list)
    end = time.time()
    prim_times.append(end - start)

    start = time.time()
    kruskal(n, edges)
    end = time.time()
    kruskal_times.append(end - start)

plt.plot(graph_sizes, prim_times, label='Prim')
plt.plot(graph_sizes, kruskal_times, label='Kruskal')
plt.xlabel('Size of graph (n)')
plt.ylabel('Execution time (seconds)')
plt.legend()
plt.show()
