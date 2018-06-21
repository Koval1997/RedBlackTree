using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RB
{
    class RedBlackTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        class RedBlackNode
        {

            private readonly T _nodeValue;
            private RedBlackNode _parentNode;
            private RedBlackNode _leftNode;
            private RedBlackNode _rightNode;

            public NodeColor Color { get; set; }

            public Direction ParentDirection
            {
                get
                {
                    if (ParentNode == null ||
                      NodeValue.CompareTo(ParentNode.NodeValue) > 0)
                        return Direction.LEFT;

                    return Direction.RIGHT;
                }
            }

            public T NodeValue
            {
                get { return _nodeValue; }
            }

            public RedBlackNode ParentNode
            {
                get { return _parentNode; }
                set { _parentNode = value; }
            }

            public RedBlackNode LeftNode
            {
                get { return _leftNode; }
                set { _leftNode = value; }
            }

            public RedBlackNode RightNode
            {
                get { return _rightNode; }
                set { _rightNode = value; }
            }

            public Boolean IsRoot
            {
                get { return (_parentNode == null); }
            }

            public Boolean IsLeaf
            {
                get
                {
                    return (_leftNode == null && _rightNode == null);
                }
            }


            public RedBlackNode(T nodeValue)
               : this(nodeValue, null, null)
            {

            }

            public RedBlackNode(T nodeValue, RedBlackNode left, RedBlackNode right)
            {
                _nodeValue = nodeValue;
                Color = NodeColor.RED;
                _leftNode = left;
                _rightNode = right;
                _parentNode = null;
            }

            public override string ToString()
            {
                return _nodeValue.ToString();
            }

            public enum NodeColor
            {
                RED = 0,
                BLACK = 1
            }

            public enum Direction
            {
                LEFT = 0,
                RIGHT = 1
            }
        }

        private RedBlackNode _rootNode;
        private int _nodeCount;
        
        public T RootNode
        {
           get { return _rootNode.NodeValue; }
       }
         
       public int NodeCount
       {
          get { return _nodeCount; }
       }
          
       public Boolean IsEmpty
       {
          get { return (_rootNode == null); }
      }
         
       public T MinValue
       {
          get
          {             
             if (IsEmpty)
                throw new Exception("Error: Cannot determine minimum value of an empty tree");     
             
             var node = _rootNode;
             while (node.LeftNode != null)
                node = node.LeftNode;
     
             return node.NodeValue;
         }
       }
          
       public T MaxValue
       {
          get
            {
                if (IsEmpty)
                throw new Exception("Error: Cannot determine maximum value of an empty tree");  
             
             var node = _rootNode;
             while (node.RightNode != null)
                node = node.RightNode;
     
             return node.NodeValue;
          }
       }
          
       public RedBlackTree()
       {
          _rootNode = null;
          }


        public void Insert(T value)
        {
            if (_rootNode == null)
            {                
                var node = new RedBlackNode(value)
                {
                    ParentNode = null, Color = RedBlackNode.NodeColor.BLACK                    
                };                
                _rootNode = node;
                _nodeCount++;
            }
            else
            {
                InsertNode(value, _rootNode);
            }
        }

        public bool Contains(T value)
    {
       if (IsEmpty)
          return false;
     
       var current = _rootNode;
       while (current != null)
       {
          switch (value.CompareTo(current.NodeValue))
          {
             case -1:
                current = current.LeftNode;
                break;
             case 1:
                current = current.RightNode;
                break;
             default:
                return true;
          }
       }
     
       return false;
    }
     
    private void InsertNode(T value, RedBlackNode current)
    {
       if (value.CompareTo(current.NodeValue) == -1)
       {
          if (current.LeftNode == null)
          {
             var node = new RedBlackNode(value)
             {
               Color = RedBlackNode.NodeColor.RED,
               ParentNode = current,
             };
             current.LeftNode = node;
             _nodeCount++;
          }
          else
          {
             InsertNode(value, current.LeftNode);
             return;
          }
       }
      else if (value.CompareTo(current.NodeValue) == 1)
       {
          if (current.RightNode == null)
          {
             var node = new RedBlackNode(value)
             {
                Color = RedBlackNode.NodeColor.RED,
                ParentNode = current,
             };
             current.RightNode = node;
             _nodeCount++;
          }
         else
          {
             InsertNode(value, current.RightNode);
             return;
          }
       }
       CheckNode(current);     
       
       _rootNode.Color = RedBlackNode.NodeColor.BLACK;
    }
     
    private void CheckNode(RedBlackNode current)
    {
       if (current == null)
          return;
     
       if (current.Color != RedBlackNode.NodeColor.RED) return;
       
       var uncleNode = GetSiblingNode(current);
       if (uncleNode != null && uncleNode.Color == RedBlackNode.NodeColor.RED)
     {
        
         uncleNode.Color = RedBlackNode.NodeColor.BLACK;
         current.Color = RedBlackNode.NodeColor.BLACK;
         current.ParentNode.Color = RedBlackNode.NodeColor.RED;    
         
         if (current.ParentNode.ParentNode != null 
           && current.ParentNode.ParentNode.NodeValue.CompareTo(_rootNode.NodeValue) != 0)
         {
            var node = current.ParentNode.ParentNode;
            CheckNode(node);
         }
      }
      else
      {
         var redChild =
            (current.LeftNode != null && current.LeftNode.Color == RedBlackNode.NodeColor.RED) 
                ? RedBlackNode.Direction.LEFT : RedBlackNode.Direction.RIGHT;    
         
         if (redChild == RedBlackNode.Direction.LEFT)
         {
            if (current.ParentDirection == RedBlackNode.Direction.RIGHT)
            {
               RotateLeftChildRightParent(current);
            }
            else
            {
               RotateLeftChildLeftParent(current);
            }
         }
         else
         {
            if (current.RightNode.Color == RedBlackNode.NodeColor.RED)
            {
               if (current.ParentDirection == RedBlackNode.Direction.RIGHT)
               {
                  RotateRightChildRightParent(current);
               }
               else
               {
                  RotateRightChildLeftParent(current);
               }
            }
         }
      }
   }
    
   private static RedBlackNode GetSiblingNode(RedBlackNode current)
   {
      if (current == null || current.ParentNode == null)
         return null;
    
      if (current.ParentNode.LeftNode != null 
        && current.ParentNode.LeftNode.NodeValue.CompareTo(current.NodeValue) == 0)
         return current.ParentNode.RightNode;
            
      return current.ParentNode.LeftNode;
   }
    
   private void FixChildColors(RedBlackNode current)
   {
      if (current.Color == RedBlackNode.NodeColor.RED)
      {
         if (current.LeftNode != null 
            && current.LeftNode.Color == RedBlackNode.NodeColor.BLACK)
         {
            current.LeftNode.Color = RedBlackNode.NodeColor.RED;
            current.Color = RedBlackNode.NodeColor.BLACK;
         }
         else if (current.RightNode != null 
            && current.RightNode.Color == RedBlackNode.NodeColor.BLACK)
        {
            current.RightNode.Color = RedBlackNode.NodeColor.RED;
            current.Color = RedBlackNode.NodeColor.BLACK;
         }
      }
   }
        
   private void RotateRightChildRightParent(RedBlackNode current)
   {     
      if (current.IsRoot)
         return;
    
      var tmpNode = current.RightNode.LeftNode;
      current.RightNode.ParentNode = current.ParentNode;
      current.ParentNode.LeftNode = current.RightNode;
      current.ParentNode = current.RightNode;
      current.RightNode.LeftNode = current;
    
      if (tmpNode != null)
      {
         current.RightNode = tmpNode;
         tmpNode.ParentNode = current;
      }
      else
      {
         current.RightNode = tmpNode;
      }    
      
      var newCurrent = current.ParentNode;
      CheckNode(newCurrent);
   }
         
   private void RotateLeftChildLeftParent(RedBlackNode current)
   {      
      if (current.IsRoot)
         return;
    
      var tmpNode = current.LeftNode.RightNode;
      current.LeftNode.ParentNode = current.ParentNode;
      current.ParentNode.RightNode = current.LeftNode;
      current.ParentNode = current.LeftNode;
      current.LeftNode.RightNode = current;
    
      if (tmpNode != null)
      {
         current.LeftNode = tmpNode;
         tmpNode.ParentNode = current;
      }
      else
      {
         current.LeftNode = tmpNode;
      }    
      
      var newCurrent = current.ParentNode;
      CheckNode(newCurrent);
   }
    
   private void RotateLeftChildRightParent(RedBlackNode current)
   {     
      if (current.IsRoot)
         return;
    
      if (current.RightNode != null)
      {
         current.ParentNode.LeftNode = current.RightNode;
         current.RightNode.ParentNode = current.ParentNode;
      }
      else
      {
         current.ParentNode.LeftNode = current.RightNode;
      }
    
      var tmpNode = current.ParentNode.ParentNode;
      current.RightNode = current.ParentNode;
      current.ParentNode.ParentNode = current;
    
      if (tmpNode == null)
      {
         _rootNode = current;
      current.ParentNode = null;
      }
      else
      {
         current.ParentNode = tmpNode;    
         
        if (tmpNode.NodeValue.CompareTo(current.NodeValue) > 0)
         {
            tmpNode.LeftNode = current;
         }
         else
         {
            tmpNode.RightNode = current;
         }
      }
    
      FixChildColors(current);    
     
      var newCurrent = current.ParentNode;
      CheckNode(newCurrent);
   }
    
   private void RotateRightChildLeftParent(RedBlackNode current)
   {      
      if (current.IsRoot)
         return;
    
      if (current.LeftNode != null)
      {
         current.ParentNode.RightNode = current.LeftNode;
         current.LeftNode.ParentNode = current.ParentNode;
      }
      else
      {
         current.ParentNode.RightNode = current.LeftNode;
      }
    
      var tmpNode = current.ParentNode.ParentNode;
      current.LeftNode = current.ParentNode;
      current.ParentNode.ParentNode = current;
    
      if (tmpNode == null)
      {
         _rootNode = current;
         current.ParentNode = null;
      }
      else
      {
         current.ParentNode = tmpNode;    
        
         if (tmpNode.NodeValue.CompareTo(current.NodeValue) > 0)
         {
            tmpNode.LeftNode = current;
         }
         else
         {
            tmpNode.RightNode = current;
         }
      }
    
      FixChildColors(current);    
      
      var newCurrent = current.ParentNode;
      CheckNode(newCurrent);
        }

        private static IEnumerable<T> InOrderTraversal(RedBlackNode node)
     {
        if (node.LeftNode != null)
       {
           foreach (T nodeVal in InOrderTraversal(node.LeftNode))
              yield return nodeVal;
        }
      
        yield return node.NodeValue;
     
       if (node.RightNode != null)
       {
          foreach (T nodeVal in InOrderTraversal(node.RightNode))
             yield return nodeVal;
       }
    }
     
    public IEnumerator<T> GetEnumerator()
    {
       foreach (T val in InOrderTraversal(_rootNode)) { yield return val; }
    }
     
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
       return GetEnumerator();
    }

    }
}

