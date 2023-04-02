import matplotlib.pyplot
import time
import graphviz as gv

balanced = {1: [2, 3], 2: [4, 5], 3: [6, 7], 4: [8, 9], 5: [10, 11], 6: [12, 13], 7: [14, 15], 8: [16, 17], 9: [18, 19], 10: [20, 21], 11: [22, 23], 12: [24, 25], 13: [26, 27], 14: [28, 29], 15: [30, 31], 16: [32, 33], 17: [34, 35], 18: [36, 37], 19: [38, 39], 20: [40, 41], 21: [42, 43], 22: [44, 45], 23: [46, 47], 24: [48, 49], 25: [50, 51], 26: [52, 53], 27: [54, 55], 28: [56, 57], 29: [58, 59], 30: [60, 61], 31: [62, 63], 32: [64, 65], 33: [66, 67], 34: [68, 69], 35: [70, 71], 36: [72, 73], 37: [74, 75], 38: [76, 77], 39: [78, 79], 40: [80, 81], 41: [82, 83], 42: [84, 85], 43: [86, 87], 44: [88, 89], 45: [90, 91], 46: [92, 93], 47: [94, 95], 48: [96, 97], 49: [98, 99], 50: [100]}
unbalanced = {1: [2, 3], 2: [4, 5], 3: [6], 4: [7, 8], 5: [9, 10], 6: [11, 12], 7: [13], 8: [14], 9: [15, 16], 11: [18], 12: [19, 20], 13: [22], 15: [23], 16: [24, 25], 17: [26, 27], 18: [28, 29], 21: [32, 33], 22: [34, 35], 24: [37, 38], 25: [39, 40], 26: [41, 42], 27: [43, 44], 28: [45, 46], 29: [47, 48], 30: [49, 50], 31: [51, 52], 32: [53, 54], 33: [55, 56], 34: [57, 58], 35: [59, 60], 36: [61, 62], 37: [63], 38: [64], 39: [65], 40: [66], 41: [67], 42: [68], 43: [69], 44: [70, 21], 45: [71], 46: [72], 47: [73], 48: [74, 30], 49: [75], 50: [76], 51: [77], 52: [78], 53: [79], 54: [80], 55: [81], 56: [82], 57: [83], 58: [84], 59: [85], 60: [86], 61: [87], 62: [88], 63: [89], 64: [90], 65: [91], 66: [92], 67: [93], 68: [94], 69: [95], 70: [96], 71: [97], 72: [98], 73: [99], 74: [100], 83: [31], 90: [36], 97: [17]}

def main():
    dfsBalancedTimes = calculateTimes(dfs, balanced, 1)
    dfsUnbalancedTimes = calculateTimes(dfs, unbalanced, 1)
    bfsBalancedTimes = calculateTimes(bfs, balanced, 1)
    bfsUnbalancedTimes = calculateTimes(bfs, unbalanced, 1)

    xValues = [i for i in getNodesFromGraph(balanced)]
    matplotlib.pyplot.plot(xValues, dfsBalancedTimes, label="DFS Balanced")
    matplotlib.pyplot.plot(xValues, dfsUnbalancedTimes, label="DFS Unbalanced")
    matplotlib.pyplot.plot(xValues, bfsBalancedTimes, label="BFS Balanced")
    matplotlib.pyplot.plot(xValues, bfsUnbalancedTimes, label="BFS Unbalanced")
    matplotlib.pyplot.xlabel("Nodes")
    matplotlib.pyplot.ylabel("time from root to each node, ms")
    matplotlib.pyplot.legend(loc = 'lower right')
    matplotlib.pyplot.show()

    # viewTree(balanced)
    # viewTree(unbalanced)



def dfs(graph, startNode, searchedNode):
    visitedNodes = set()
    stack = [startNode]
    while stack:
        node = stack.pop()
        if node == searchedNode:
            return

        if node in visitedNodes:
            continue

        visitedNodes.add(node)
        if (node not in graph.keys()):
            continue

        subnodes = graph[node]
        for subnode in subnodes:
            if subnode not in visitedNodes:
                stack.append(subnode)

def bfs(graph, startNode, searchedNode):
    visited = set()
    queue = [startNode]
    while queue:
        node = queue.pop(0)
        if node in visited:
            continue
        visited.add(node)
        if node == searchedNode:
            return

        if (node not in graph.keys()):
            continue

        subnodes = graph[node]
        for subnode in subnodes:
            queue.append(subnode)

def calculateTimes(algorithm, graph, startNode):
    allNodes = getNodesFromGraph(graph)
    timeForEachNode = [0]
    for nodeToSearch in allNodes:
        start = time.time()
        algorithm(graph, startNode, nodeToSearch)
        end = time.time()
        searchTimeMs = round((end - start) * 1000, 3)
        timeForEachNode.append(timeForEachNode[nodeToSearch-1] + searchTimeMs)
    timeForEachNode.remove(0)
    return timeForEachNode

def getNodesFromGraph(graph):
    allNodes = set()
    for (node, subnodes) in graph.items():
        allNodes.add(node)
        for subNode in subnodes:
            allNodes.add(subNode)
    return allNodes

def viewTree(tree):
    dot = gv.Digraph()
    for key in tree.keys():
        dot.node(str(key))
        if tree[key]:
            for value in tree[key]:
                dot.edge(str(key), str(value))
    dot.view()

if __name__ == "__main__":
    main()