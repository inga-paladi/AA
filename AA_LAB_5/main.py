import sys
from random import randint
from matplotlib import pyplot as plt
from time import time


# Dijkstra algorithm(the shortest path from the source)
def dijkstraAlgorithm(vertices, edges, source):
    num_of_vertices = len(vertices)
    visited_and_distance = [(0, sys.maxsize) if i != source else (0, 0) for i in range(num_of_vertices)]
    for vertex in range(num_of_vertices):
        to_visit = min(filter(lambda i: not visited_and_distance[i][0], range(num_of_vertices)),
                       key=lambda i: visited_and_distance[i][1], default=None)
        if to_visit is None:
            break
        for neighbor_index in range(num_of_vertices):
            if vertices[to_visit][neighbor_index] == 1 and visited_and_distance[neighbor_index][0] == 0:
                new_distance = visited_and_distance[to_visit][1] + edges[to_visit][neighbor_index]
                if visited_and_distance[neighbor_index][1] > new_distance:
                    visited_and_distance[neighbor_index] = (0, new_distance)
        visited_and_distance[to_visit] = (1, visited_and_distance[to_visit][1])
    return visited_and_distance


def Floyd_Warshall_algorithm(vertices, edges):
    n = len(vertices)
    distance = edges.copy()
    for k in range(n):
        for i in range(n):
            for j in range(n):
                distance[i][j] = min(distance[i][j], distance[i][k] + distance[k][j])
    return distance





def generateGraph(size, coef):
    vertices = [[0] * size for _ in range(size)]
    edges = [[0] * size for _ in range(size)]

    for i in range(size):
        for j in range(i + 1, size):
            prob = randint(1, 100)
            if prob <= coef:
                weight = randint(10, 100)
                vertices[i][j] = 1
                vertices[j][i] = 1
                edges[i][j] = weight
                edges[j][i] = weight

    return vertices, edges


def currentTime():
    return time() * 1000


def normalizeVerticesSet(n_vertices, vertices, edges):
    for x in range(n_vertices):
        for y in range(n_vertices):
            if (vertices[x][y] == 0) and (x != y):
                edges[x][y] = sys.maxsize


# coefficients for defining the number of edges in a graph
input_sizes = [10, 50, 100, 150, 200, 250, 300, 350, 400]
for n in input_sizes:
    m = (n*(n-1))/2
    dense_coefficient = (2*m) / (n*(n-1))
    sparse_coefficient = (n*n - m) / (n*n)


dijkstra_dense, dijkstra_sparse = [], []
floyd_marshall_dense, floyd_sparse = [], []
start_time, end_time = 0, 0

for input_size in input_sizes:
    vertices, edges = generateGraph(input_size, dense_coefficient)

    # Time Dijkstra's algorithm
    start_time = currentTime()
    for k in range(input_size):
        dijkstraAlgorithm(vertices, edges, k)
    dijkstra_dense.append(round(currentTime() - start_time, 3))

    # Time Floyd-Warshall algorithm
    start_time = currentTime()
    Floyd_Warshall_algorithm(vertices, edges)
    floyd_marshall_dense.append(round(currentTime() - start_time, 3))

    # Normalize edges
    normalizeVerticesSet(input_size, vertices, edges)

for input_size in input_sizes:
    vertices, edges = generateGraph(input_size, sparse_coefficient)

    # Time Dijkstra's algorithm
    start_time = currentTime()
    for k in range(input_size):
        dijkstraAlgorithm(vertices, edges, k)
    dijkstra_sparse.append(round(currentTime() - start_time, 3))

    # Normalize edges
    normalizeVerticesSet(len(vertices[0]), vertices, edges)

    # Time Floyd-Warshall algorithm
    start_time = currentTime()
    Floyd_Warshall_algorithm(vertices, edges)
    floyd_sparse.append(round(currentTime() - start_time, 3))

algorithms = ["Dijkstra", "Floyd_Warshall"]
dense_data = [dijkstra_dense, floyd_marshall_dense]
sparse_data = [dijkstra_sparse, floyd_sparse]
colors = ["red", "blue"]

for i, algorithm in enumerate(algorithms):
    plt.figure()
    plt.plot(input_sizes, dense_data[i], color=colors[0], label="Dense graph")
    plt.plot(input_sizes, sparse_data[i], color=colors[1], label="Sparse graph")
    plt.title(f"{algorithm} Algorithm", color="black", fontsize=16)
    plt.legend(loc='upper left')
    plt.xlabel(" Size ", color="black", fontsize=14)
    plt.ylabel("Time (millis)", color="black", fontsize=14)
    plt.grid()
    plt.show()


plt.plot(input_sizes, dijkstra_dense, color="red", label="Dijkstra")
plt.plot(input_sizes, floyd_marshall_dense, color="blue", label="Floyd-Warshall")
plt.title("Dense graphs", color="black", fontsize=16)
plt.legend(loc='upper left')
plt.xlabel(" Size ", color="black", fontsize=14)
plt.ylabel("Time (millis)", color="black", fontsize=14)
plt.grid()
plt.show()

plt.plot(input_sizes, dijkstra_sparse, color="red", label="Dijkstra")
plt.plot(input_sizes, floyd_sparse, color="blue", label="Floyd-Warshall")
plt.title("Sparse graphs", color="black", fontsize=16)
plt.legend(loc='upper left')
plt.xlabel(" Size ", color="black", fontsize=14)
plt.ylabel("Time (millis)", color="black", fontsize=14)
plt.grid()
plt.show()

# Printing the time records
algorithms = ['Dijkstra', 'Floyd-Warshall']
graph_types = ['dense', 'sparse']

for algorithm in algorithms:
    for graph_type in graph_types:
        times = globals()[f'{algorithm.lower()}_{graph_type}']
        print(f"{algorithm}'s algorithm on {graph_type} graphs:")
        for i, size in enumerate(input_sizes):
            print(f"Graph size: {size}")
            print(f"Time taken: {times[i]} ms")
            print()




