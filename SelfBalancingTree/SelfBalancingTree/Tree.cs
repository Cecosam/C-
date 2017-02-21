using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SelfBalancingTree
{
    [Serializable]
    public class Tree<T> : ISet<T>, ICollection<T>, IEnumerable<T> where T : IComparable<T>
    {
        [Serializable]
        public class Node<T>
        {
            public Node(T item, Node<T> parent)
            {
                this.Parent = parent;
                this.LeftNode = null;
                this.RightNode = null;
                this.Value = item;
            }
            private Node()
            {

            }
            public Node<T> LeftNode { get; set; }

            public Node<T> RightNode { get; set; }
            public Node<T> Parent { get; set; }
            public T Value { get; set; }
        }

        private T minValue;
        private T maxValue;
        private int currentMaxLevel = 0;

        public Tree(IEnumerable<T> collection, bool selfBalancing = true)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
             
            foreach (var item in collection)
            {
                this.Add(item);
            }
            this.IsSelfBalancing = selfBalancing;
        }
        public Tree(T item, bool selfBalancing = true)
        {
            this.Root = new Node<T>(item, null);
            this.Count = 1;
            this.IsSelfBalancing = selfBalancing;
        }
        public Tree(bool selfBalancing = true)
        {
            this.Root = null;
            this.Count = 0;
            this.IsSelfBalancing = selfBalancing;
        }
        public int Count { get; private set; }
        public T MaxValue
        {
            get
            {
                if (this.Root == null)
                {
                    throw new ArgumentNullException("The Tree is empty!");
                }
                return this.maxValue;
            }
            private set
            {
                this.maxValue = value;
            }
        }
        public T MinValue
        {
            get
            {
                if (this.Root == null)
                {
                    throw new ArgumentNullException("The Tree is empty!");
                }
                return this.minValue;
            }
            private set
            {
                this.minValue = value;
            }
        }
        private bool IsSelfBalancing { get; set; }
        private Node<T> Root { get; set; }      
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public bool Add(T item)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(item, null);
                this.Count++;
                this.MinValue = item;
                this.MaxValue = item;
                return true;
            }

            var currentNode = this.Root;

            while (true)
            {
                var value = currentNode.Value.CompareTo(item);
                if (value > 0)
                {
                    if (currentNode.LeftNode == null)
                    {
                        currentNode.LeftNode = new Node<T>(item, currentNode);
                        if (item.CompareTo(MinValue) < 0)
                        {                            
                            this.MinValue = item; 
                        }
                        this.Count++;
                        if (this.IsSelfBalancing)
                        {
                            BalanceTree();
                        }
                        return true;
                    }
                    else
                    {
                        currentNode = currentNode.LeftNode;
                    }
                }
                else if (value < 0)
                {
                    if (currentNode.RightNode == null)
                    {
                        currentNode.RightNode = new Node<T>(item, currentNode);
                        if (item.CompareTo(MaxValue) > 0)
                        {
                            this.MaxValue = item;
                        }
                        this.Count++;
                        if (this.IsSelfBalancing)
                        {
                            BalanceTree();
                        }
                        return true;
                    }
                    else
                    {
                        currentNode = currentNode.RightNode;
                    }
                }
                else if (value == 0)
                {
                    return false;
                }
            }
        }
        void ICollection<T>.Add(T item)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(item, null);
                this.Count++;
                return;
            }

            var currentNode = this.Root;

            while (true)
            {
                var value = currentNode.Value.CompareTo(item);
                if (value > 0)
                {
                    if (currentNode.LeftNode == null)
                    {
                        currentNode.LeftNode = new Node<T>(item, currentNode);
                        this.Count++;
                        if (this.IsSelfBalancing)
                        {
                            BalanceTree();
                        }
                        return;
                    }
                    else
                    {
                        currentNode = currentNode.LeftNode;
                    }
                }
                else if (value < 0)
                {
                    if (currentNode.RightNode == null)
                    {
                        currentNode.RightNode = new Node<T>(item, currentNode);
                        this.Count++;
                        if (this.IsSelfBalancing)
                        {
                            BalanceTree();
                        }
                        return;
                    }
                    else
                    {
                        currentNode = currentNode.RightNode;
                    }
                }
                else if (value == 0)
                {
                    return;
                }
            }
        }
        public bool Remove(T item)
        {
            var currentNode = this.Root;

            while (true)
            {
                if (currentNode == null)
                {
                    return false;
                }
                var value = currentNode.Value.CompareTo(item);
                if (value > 0)
                {
                    currentNode = currentNode.LeftNode;
                }
                else if (value < 0)
                {
                    currentNode = currentNode.RightNode;
                }
                else if (value == 0)
                {
                    CheckIfMinValue(currentNode);
                    CheckIfMaxValue(currentNode);
                    RemoveElement(currentNode);
                    this.Count--;
                    if (this.IsSelfBalancing)
                    {
                        BalanceTree();
                    }
                    return true;
                }
            }
        }        
        public bool Contains(T item)
        {
            var currentNode = this.Root;

            while (true)
            {
                if (currentNode == null)
                {
                    return false;
                }

                var value = currentNode.Value.CompareTo(item);
                if (value > 0)
                {
                    currentNode = currentNode.LeftNode;
                }
                else if (value < 0)
                {
                    currentNode = currentNode.RightNode;
                }
                else if (value == 0)
                {
                    return true;
                }
            }
        }
        public void Clear()
        {
            this.Root = null;
            this.Count = 0;
        }
        public void CopyTo(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }
            var list = new List<T>();

            GetAllElements(this.Root, list);

            if (array.Length < list.Count)
            {
                throw new ArgumentException("The capacity of the input array is too small");
            }

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentException();
            }

            var list = new List<T>();

            GetAllElements(this.Root, list);

            if (array.Length < arrayIndex + list.Count)
            {
                throw new ArgumentException("The capacity of the input array is too small");
            }

            for (int i = arrayIndex; i < list.Count + arrayIndex; i++)
            {
                array[i] = list[i - arrayIndex];
            }
        }      
        public void PrintElements()
        {
            if (this.Root == null)
            {
                return;
            }

            Print(this.Root);
        }
        public IEnumerable<T> Reverse()
        {
            var list = new List<T>();

            GetReversedListOfElements(this.Root, list);

            return list as IEnumerable<T>;
        }      
        public IEnumerator<T> GetEnumerator()
        {
            var list = new List<T>();

            GetAllElements(this.Root, list);

            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            var list = new List<T>();

            GetAllElements(this.Root, list);

            for (int i = 0; i < list.Count - 1; i++)
            {
                result.Append(list[i].ToString() + ", ");
            }
            result.Append(list[list.Count-1].ToString() + "!");

            return result.ToString();
        }
        public HashSet<T> ToHashSet()
        {
            var result = new HashSet<T>();

            GetAllElements(this.Root, result);

            return result;
        }
        public List<T> ToList()
        {
            var result = new List<T>();

            GetAllElements(this.Root, result);

            return result;
        }
        public string IsPerfectlyBalancedAVLTree()
        {
            if (this.Root == null)
            {
                return null;
            }

            double notAVLSubtree = 0d;
            var allSubrtrees = 1;
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                

                if (IsBalanced(currentNode) != 0)
                {
                    notAVLSubtree++;
                }

                if (currentNode.LeftNode == null && currentNode.RightNode == null)
                {
                    continue;
                }

                if (currentNode.LeftNode != null)
                {
                    allSubrtrees++;
                    queue.Enqueue(currentNode.LeftNode);
                }
                if (currentNode.RightNode != null)
                {
                    allSubrtrees++;
                    queue.Enqueue(currentNode.RightNode);
                }
            }

            return string.Format("Balanced at {0:F2}%", ((allSubrtrees - notAVLSubtree) / allSubrtrees) * 100);
        }
        public void EnableSelfBalancing()
        {
            this.IsSelfBalancing = true;
            BalanceTree();
        }
        public void DisableSelfBalancing()
        {
            this.IsSelfBalancing = false;
        }
        public IEnumerable<int> GetTopLevels()
        {
            var result = new List<int>();

            GetAllTopLevels(result, this.Root);

            return result;
        }
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var item in other)
            {
                this.Add(item);
            }
        }
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in this)
            {
                if (other.Contains(item))
                {
                    continue;
                }
                this.Remove(item);
            }

            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    continue;
                }
                this.Add(item);
            }
        }
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    this.Remove(item);
                }
            }
        }
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            var setWithItemsToRemove = new HashSet<T>();
            var setWithItemsToAdd = new HashSet<T>();

            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    setWithItemsToRemove.Add(item);
                }
                else
                {
                    setWithItemsToAdd.Add(item);
                }
            }

            foreach (var item in setWithItemsToAdd)
            {
                this.Add(item);
            }

            foreach (var item in setWithItemsToRemove)
            {
                this.Remove(item);
            }
        }
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Count > other.Count())
            {
                return false;
            }

            var array = other.ToArray();

            for (int i = 0; i <= array.Length - this.Count; i++)
            {
                var areEqual = true;
                var counter = 0;
                foreach (var item in this)
                {
                    if (item.CompareTo(array[i + counter]) != 0)
                    {
                        areEqual = false;
                        break;
                    }
                    counter++;
                }
                if (areEqual)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Count < other.Count())
            {
                return false;
            }

            var list = this.ToList();

            for (int i = 0; i <= list.Count - other.Count(); i++)
            {
                var areEqual = true;
                var counter = 0;
                foreach (var item in other)
                {
                    if (item.CompareTo(list[i + counter]) != 0)
                    {
                        areEqual = false;
                        break;
                    }
                    counter++;
                }
                if (areEqual)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Count <= other.Count())
            {
                return false;
            }

            var list = this.ToList();
            var counter = 0;
            var isPropSuperset = false;

            foreach (var item in other)
            {
                isPropSuperset = false;
                for (int i = counter; i <= list.Count - 1; i++)
                {
                    if (item.CompareTo(list[i]) == 0)
                    {
                        isPropSuperset = true;
                        counter = i + 1;
                        break;
                    }
                }
                if (!isPropSuperset)
                {
                    return false;
                }
            }

            return isPropSuperset;
        }
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Count >= other.Count())
            {
                return false;
            }

            var list = other.ToList();
            var counter = 0;
            var isPropSuperset = false;

            foreach (var item in this)
            {
                isPropSuperset = false;
                for (int i = counter; i <= list.Count - 1; i++)
                {
                    if (item.CompareTo(list[i]) == 0)
                    {
                        isPropSuperset = true;
                        counter = i + 1;
                        break;
                    }
                }
                if (!isPropSuperset)
                {
                    return false;
                }
            }

            return isPropSuperset;
        }
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Count.CompareTo(other.Count()) != 0)
            {
                return false;
            }

            var isThere = false;

            foreach (var item in other)
            {
                isThere = false;
                foreach (var value in this)
                {
                    if (value.CompareTo(item) == 0)
                    {
                        isThere = true;
                        break;
                    }
                }
                if (!isThere)
                {
                    return false;
                }
            }

            return true;
        }
        public Tree<T> DeepCopyOfThisTree()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Tree<T>)formatter.Deserialize(ms);
            }
        }
        private void GetAllElements(Node<T> currentNode, ICollection<T> result)
        {
            if (currentNode == null)
            {
                return;
            }
            if (currentNode.LeftNode == null && currentNode.RightNode == null)
            {
                result.Add(currentNode.Value);
                return;
            }

            if (currentNode.LeftNode != null)
            {
                GetAllElements(currentNode.LeftNode, result);
            }

            result.Add(currentNode.Value);

            if (currentNode.RightNode != null)
            {
                GetAllElements(currentNode.RightNode, result);
            }
        }
        private void GetReversedListOfElements(Node<T> currentNode, List<T> result)
        {
            if (currentNode == null)
            {
                return;
            }

            if (currentNode.LeftNode == null && currentNode.RightNode == null)
            {
                result.Add(currentNode.Value);
                return;
            }

            if (currentNode.RightNode != null)
            {
                GetReversedListOfElements(currentNode.RightNode, result);
            }

            result.Add(currentNode.Value);

            if (currentNode.LeftNode != null)
            {
                GetReversedListOfElements(currentNode.LeftNode, result);
            }
        }
        private void GetAllTopLevels(List<int> result, Node<T> node, int counter = 0)
        {
            if (node == null)
            {
                return;
            }
            if (node.LeftNode == null && node.RightNode == null)
            {
                result.Add(counter);
                return;
            }

            if (node.LeftNode != null)
            {
                GetAllTopLevels(result, node.LeftNode, counter + 1);
            }

            if (node.RightNode != null)
            {
                GetAllTopLevels(result, node.RightNode, counter + 1);
            }
        }
        private void RemoveElement(Node<T> nodeToRemove)
        {
            if (nodeToRemove.LeftNode == null && nodeToRemove.RightNode == null)
            {
                RemoveElementWithNoChilds(nodeToRemove);
                return;
            }
            else if (nodeToRemove.Parent == null)
            {
                RemoveRootElement(nodeToRemove);
                return;
            }
            else if (nodeToRemove.Equals(nodeToRemove.Parent.LeftNode))
            {
                RemoveWhenParentPointsToTheLeft(nodeToRemove);
                return;
            }
            else if (nodeToRemove.Equals(nodeToRemove.Parent.RightNode))
            {
                RemoveWhenParentPointsToTheRight(nodeToRemove);
                return;
            }
        }
        private void RemoveRootElement(Node<T> nodeToRemove)
        {
            if (nodeToRemove.LeftNode == null)
            {
                var nodeToReplaceTheOldOne = GetLeftMostElement(nodeToRemove.RightNode);

                if (nodeToReplaceTheOldOne.Equals(nodeToRemove.RightNode))
                {
                    this.Root = nodeToReplaceTheOldOne;
                    nodeToReplaceTheOldOne.Parent = null;
                }
                else
                {
                    var nodeToTheRight = nodeToRemove.RightNode;
                    var nodeToTheRightOfTheNewNode = nodeToReplaceTheOldOne.RightNode;
                    var nodeParentOfTheNewNode = nodeToReplaceTheOldOne.Parent;

                    nodeParentOfTheNewNode.LeftNode = nodeToTheRightOfTheNewNode;
                    if (nodeToTheRightOfTheNewNode != null)
                    {
                        nodeToTheRightOfTheNewNode.Parent = nodeParentOfTheNewNode;
                    }

                    this.Root = nodeToReplaceTheOldOne;

                    nodeToReplaceTheOldOne.Parent = null;

                    nodeToReplaceTheOldOne.RightNode = nodeToTheRight;
                    nodeToTheRight.Parent = nodeToReplaceTheOldOne;
                    //nodeToReplaceTheOldOne.LeftNode = nodeToRemove.LeftNode;
                    //nodeToRemove.LeftNode.Parent = nodeToReplaceTheOldOne;
                }
            }
            else
            {
                var nodeToReplaceTheOldOne = GetRightMostElement(nodeToRemove.LeftNode);
                var nodeToTheRightOfTheOldNode = nodeToRemove.RightNode;

                if (nodeToReplaceTheOldOne.Equals(nodeToRemove.LeftNode))
                {
                    this.Root = nodeToReplaceTheOldOne;
                    nodeToReplaceTheOldOne.Parent = null;

                    nodeToReplaceTheOldOne.RightNode = nodeToTheRightOfTheOldNode;
                    if (nodeToTheRightOfTheOldNode != null)
                    {
                        nodeToTheRightOfTheOldNode.Parent = nodeToReplaceTheOldOne;
                    }
                }
                else
                {
                    var nodeToTheLeft = nodeToRemove.LeftNode;
                    var nodeToTheLeftOfTheNewNode = nodeToReplaceTheOldOne.LeftNode;
                    var nodeParentOfTheNewNode = nodeToReplaceTheOldOne.Parent;

                    nodeParentOfTheNewNode.RightNode = nodeToTheLeftOfTheNewNode;
                    if (nodeToTheLeftOfTheNewNode != null)
                    {
                        nodeToTheLeftOfTheNewNode.Parent = nodeParentOfTheNewNode;
                    }

                    this.Root = nodeToReplaceTheOldOne;
                    nodeToReplaceTheOldOne.Parent = null;

                    nodeToReplaceTheOldOne.LeftNode = nodeToTheLeft;
                    nodeToTheLeft.Parent = nodeToReplaceTheOldOne;

                    nodeToReplaceTheOldOne.RightNode = nodeToTheRightOfTheOldNode;
                    if (nodeToTheRightOfTheOldNode != null)
                    {
                        nodeToTheRightOfTheOldNode.Parent = nodeToReplaceTheOldOne;
                    }
                }
            }
        }
        private void RemoveWhenParentPointsToTheRight(Node<T> nodeToRemove)
        {
            var nodeToTheLeftOfTheOneToRemove = nodeToRemove.LeftNode;

            if (nodeToTheLeftOfTheOneToRemove == null)
            {
                nodeToRemove.Parent.RightNode = nodeToRemove.RightNode;
                nodeToRemove.RightNode.Parent = nodeToRemove.Parent;

                return;
            }

            var elementToReplaceTheOldOne = GetRightMostElement(nodeToTheLeftOfTheOneToRemove);
            var theElementToTheRightOfTheOneToRemove = nodeToRemove.RightNode;    //null?
            var elementToTheLeftOfTheNewElement = elementToReplaceTheOldOne.LeftNode;  //null?
            if (elementToReplaceTheOldOne.Equals(nodeToTheLeftOfTheOneToRemove))
            {
                nodeToRemove.Parent.RightNode = elementToReplaceTheOldOne;
                elementToReplaceTheOldOne.Parent = nodeToRemove.Parent;

                elementToReplaceTheOldOne.RightNode = theElementToTheRightOfTheOneToRemove;
                if (theElementToTheRightOfTheOneToRemove != null)
                {
                    theElementToTheRightOfTheOneToRemove.Parent = elementToReplaceTheOldOne;
                }

                return;
            }

            var nodeBeforeTheNewElement = elementToReplaceTheOldOne.Parent;

            nodeToRemove.Parent.RightNode = elementToReplaceTheOldOne;
            elementToReplaceTheOldOne.Parent = nodeToRemove.Parent;

            elementToReplaceTheOldOne.LeftNode = nodeToTheLeftOfTheOneToRemove;
            nodeToTheLeftOfTheOneToRemove.Parent = elementToReplaceTheOldOne;

            elementToReplaceTheOldOne.RightNode = theElementToTheRightOfTheOneToRemove;
            if (theElementToTheRightOfTheOneToRemove != null)
            {
                theElementToTheRightOfTheOneToRemove.Parent = elementToReplaceTheOldOne;
            }

            nodeBeforeTheNewElement.RightNode = elementToTheLeftOfTheNewElement;
            if (elementToTheLeftOfTheNewElement != null)
            {
                elementToTheLeftOfTheNewElement.Parent = nodeBeforeTheNewElement;
            }
        }
        private void RemoveWhenParentPointsToTheLeft(Node<T> nodeToRemove)
        {
            var nodeToTheRightOfTheOneToRemove = nodeToRemove.RightNode;

            if (nodeToTheRightOfTheOneToRemove == null)
            {
                nodeToRemove.Parent.LeftNode = nodeToRemove.LeftNode;
                nodeToRemove.LeftNode.Parent = nodeToRemove.Parent;

                return;
            }

            var elementToReplaceTheOldOne = GetLeftMostElement(nodeToTheRightOfTheOneToRemove);
            var theElementToTheLeftOfTheOneToRemove = nodeToRemove.LeftNode;
            var elementToTheRightOfTheNewElement = elementToReplaceTheOldOne.RightNode;

            if (elementToReplaceTheOldOne.Equals(nodeToTheRightOfTheOneToRemove))
            {
                nodeToRemove.Parent.LeftNode = elementToReplaceTheOldOne;
                elementToReplaceTheOldOne.Parent = nodeToRemove.Parent;

                elementToReplaceTheOldOne.LeftNode = theElementToTheLeftOfTheOneToRemove;
                if (theElementToTheLeftOfTheOneToRemove != null)
                {
                    theElementToTheLeftOfTheOneToRemove.Parent = elementToReplaceTheOldOne;
                }

                return;
            }

            var nodeBeforeTheNewElement = elementToReplaceTheOldOne.Parent;

            nodeToRemove.Parent.LeftNode = elementToReplaceTheOldOne;
            elementToReplaceTheOldOne.Parent = nodeToRemove.Parent;

            elementToReplaceTheOldOne.RightNode = nodeToTheRightOfTheOneToRemove;
            nodeToTheRightOfTheOneToRemove.Parent = elementToReplaceTheOldOne;

            elementToReplaceTheOldOne.LeftNode = theElementToTheLeftOfTheOneToRemove;
            if (theElementToTheLeftOfTheOneToRemove != null)
            {
                theElementToTheLeftOfTheOneToRemove.Parent = elementToReplaceTheOldOne;
            }

            nodeBeforeTheNewElement.LeftNode = elementToTheRightOfTheNewElement;
            if (elementToTheRightOfTheNewElement != null)
            {
                elementToTheRightOfTheNewElement.Parent = nodeBeforeTheNewElement;
            }
        }
        private void RemoveElementWithNoChilds(Node<T> elementToRemove)
        {
            if (elementToRemove.Parent == null)
            {
                this.Root = null;
            }
            else if (elementToRemove.Equals(elementToRemove.Parent.LeftNode))
            {
                elementToRemove.Parent.LeftNode = null;
            }
            else
            {
                elementToRemove.Parent.RightNode = null;
            }
        }
        private void Print(Node<T> currentNode)
        {
            if (currentNode.LeftNode == null && currentNode.RightNode == null)
            {
                Console.Write("{0}, ", currentNode.Value);
                return;
            }

            if (currentNode.LeftNode != null)
            {
                Print(currentNode.LeftNode);
            }

            Console.Write("{0}, ", currentNode.Value);

            if (currentNode.RightNode != null)
            {
                Print(currentNode.RightNode);
            }
        }
        private void BalanceTree()
        {
            if (this.Root == null)
            {
                return;
            }
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                
                currentNode = Rotation(currentNode);

                if (currentNode.LeftNode != null)
                {
                    queue.Enqueue(currentNode.LeftNode);
                }
                if (currentNode.RightNode != null)
                {
                    queue.Enqueue(currentNode.RightNode);
                }
            }

            if (IsBalanced(this.Root) != 0)
            {
                BalanceTree();
            }
        }       
        private int IsBalanced(Node<T> subTreeRoot)
        {
            var leftSideLevel = 0;
            if (subTreeRoot.LeftNode != null)
            {
                GetMaxLevel(subTreeRoot.LeftNode);
            }
            leftSideLevel = this.currentMaxLevel;
            this.currentMaxLevel = 0;

            var rightSideLevel = 0;
            if (subTreeRoot.RightNode != null)
            {
                GetMaxLevel(subTreeRoot.RightNode);
            }
            rightSideLevel = this.currentMaxLevel;
            this.currentMaxLevel = 0;


            if (leftSideLevel >= rightSideLevel + 2)
            {
                return -1;
            }
            else if (leftSideLevel + 2 <= rightSideLevel)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private void GetMaxLevel(Node<T> currentNode, int counter = 1)
        {
            if (currentNode.LeftNode == null && currentNode.RightNode == null)
            {
                if (counter > currentMaxLevel)
                {
                    currentMaxLevel = counter;
                }
                return;
            }

            if (currentNode.LeftNode != null)
            {
                GetMaxLevel(currentNode.LeftNode, counter + 1);
            }

            if (currentNode.RightNode != null)
            {
                GetMaxLevel(currentNode.RightNode, counter + 1);
            }
        }
        private Node<T> Rotation(Node<T> currentNode)
        {
            var isBalanced = IsBalanced(currentNode);

            while (isBalanced != 0)
            {
                if (isBalanced == 1)
                {
                    currentNode = RotationToLeft(currentNode);
                }
                else if (isBalanced == -1)
                {
                    currentNode = RotationToRight(currentNode);
                }
                isBalanced = IsBalanced(currentNode);
            }

            return currentNode;
        }
        private Node<T> RotationToLeft(Node<T> subRootNode)
        {
            var rightNode = subRootNode.RightNode;
            if (rightNode.LeftNode != null)
            {
                var leftMostElement = GetLeftMostElement(rightNode);
                var rightMostElementOfTheLeftMostElement = GetRightMostElement(leftMostElement);

                leftMostElement.Parent.LeftNode = null;
                leftMostElement.LeftNode = subRootNode;
                leftMostElement.Parent = subRootNode.Parent;
                ConnectParrentAndChild(subRootNode, leftMostElement);
                subRootNode.Parent = leftMostElement;
                subRootNode.RightNode = null;
                rightNode.Parent = rightMostElementOfTheLeftMostElement;
                rightMostElementOfTheLeftMostElement.RightNode = rightNode;

                return leftMostElement;
            }
            else
            {
                rightNode.LeftNode = subRootNode;
                rightNode.Parent = subRootNode.Parent;
                ConnectParrentAndChild(subRootNode, rightNode);
                subRootNode.Parent = rightNode;
                subRootNode.RightNode = null;

                return rightNode;
            }
        }
        private Node<T> RotationToRight(Node<T> subRootNode)
        {
            var leftNode = subRootNode.LeftNode;
            if (leftNode.RightNode != null)
            {
                var rightMostElement = GetRightMostElement(leftNode);
                var leftMostElementOfTheRightMostElement = GetLeftMostElement(rightMostElement);

                rightMostElement.Parent.RightNode = null;
                rightMostElement.RightNode = subRootNode;
                rightMostElement.Parent = subRootNode.Parent;
                ConnectParrentAndChild(subRootNode, rightMostElement);
                subRootNode.Parent = rightMostElement;
                subRootNode.LeftNode = null;
                leftNode.Parent = leftMostElementOfTheRightMostElement;
                leftMostElementOfTheRightMostElement.LeftNode = leftNode;

                return rightMostElement;
            }
            else
            {
                leftNode.RightNode = subRootNode;
                leftNode.Parent = subRootNode.Parent; ;
                ConnectParrentAndChild(subRootNode, leftNode);
                subRootNode.Parent = leftNode;
                subRootNode.LeftNode = null;

                return leftNode;
            }
        }
        private void ConnectParrentAndChild(Node<T> parent, Node<T> sibling)
        {
            if (parent.Parent == null)
            {
                this.Root = sibling;
                return;
            }
            if (parent.Parent.LeftNode.Equals(parent))
            {
                parent.Parent.LeftNode = sibling;
            }
            else
            {
                parent.Parent.RightNode = sibling;
            }
        }
        private Node<T> GetLeftMostElement(Node<T> node)
        {
            var currentNode = node;
            while (true)
            {
                if (currentNode.LeftNode == null)
                {
                    return currentNode;
                }
                else
                {
                    currentNode = currentNode.LeftNode;
                }
            }
        }
        private Node<T> GetRightMostElement(Node<T> node)
        {
            var currentNode = node;
            while (true)
            {
                if (currentNode.RightNode == null)
                {
                    return currentNode;
                }
                else
                {
                    currentNode = currentNode.RightNode;
                }
            }
        }
        private void CheckIfMaxValue(Node<T> currentNode)
        {
            if (currentNode.Value.CompareTo(this.MaxValue) == 0)
            {
                if (currentNode.LeftNode != null)
                {
                    var leftNode = currentNode.LeftNode;
                    this.MaxValue = GetRightMostElement(leftNode).Value;
                }
                else
                {
                    if (currentNode.Parent != null)
                    {
                        this.MaxValue = currentNode.Parent.Value;
                    }
                }
            }
            else
            {
                return;
            }
        }
        private void CheckIfMinValue(Node<T> currentNode)
        {
            if (currentNode.Value.CompareTo(this.MinValue) == 0)
            {
                if (currentNode.RightNode != null)
                {
                    var rightNode = currentNode.RightNode;
                    this.MinValue = GetLeftMostElement(rightNode).Value;
                }
                else
                {
                    if (currentNode.Parent != null)
                    {
                        this.MinValue = currentNode.Parent.Value;
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
