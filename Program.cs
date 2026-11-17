/* Part 1
Question 1: A balanced tree is where one side isn't less completed than the other, we use the Balance factor to determine if it is
Question 2: Height of 5, takes 5 nodes to find 5
If it were balanced it would be heihgt of 3
10
    20
        30
            40
                50
Question 3: The balance factor is the difference between the height of the left and right subtrees. If the balance factor is greater than 1 or less than -1, then the tree is unbalanced.
Question 4: An AVL tree is log(n) because each time we check a level, we cut the search in half as values greater than current go on one side and less than the other.
A plain BST is O(n) because there is no guarantee the items will be sorted, they just have to be less than the parent node
Question 5: Height is 3, it is balanced
*/
/* Part 2
Question 6: Insert 33, 18, 7, 25, 29
LL around 7, 18, and 33
RL around 18, 7, and 25 
Final tree:
          25
        18     33
      7      29
Question 7: Insert 14, 26, 38, 44, 41
RR aound 14, 26, 38
RR around 36, 44, 48
Final tree:
          26
        14     44
            38    48
Question 8: Insert 42 into this tree

        71
      48      90
    39
LR rotation around 48,39,42
Final tree:
        71
      42      90
    39    48
Question 9: Insert 52, 37, 68, 29, 44, 41, 75, 80, 61,78
First 5 insertions are fine
LL rotation around 41.44.68
RR rotation around 68, 75, 80
LL rotation around 61, 68, 78
RL rotation around 52, 44, 41
Final tree:
            41
        37      52
      29      44     68
                61     75
                      78    80
*/
public class Program
{
    public static void Main(string[] args)
    {
       int values = {52, 37,68, 29, 44, 41, 75, 80, 61, 78 };
       foreach( int value in values)
       {
        root = Insert(root, value);
       }  
        int values2 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}

        foreach(value in values2)
        {
            root = Insert(root, value);
        }


    }

    public class AvlNode
    {
        public int Value;
        public AvlNode Left;
        public AvlNode Right;
        public int Height;

        public AvlNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Height = 1; // New node is initially added at leaf
        }

        public int GetHeight(AvlNode node)
        {
            if (node == null)
            {
                return -1;
            }
            return node.Height;
        }
        public int GetBalanceFactor(AvlNode node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }
        public void UpdateHeight(AvlNode node)
        {
            if (node != null)
            {
                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            }
        }
       public AvlNode RotateRight(AvlNode node)
        {
            if (node == null || node.Left == null)
            {
                return node;
            }

            AvlNode temp = node.Left;

            node.Left = temp.Right;

            temp.Right = node;

            UpdateHeight(node);
            UpdateHeight(temp);

            return temp;
        }
        public AvlNode RotateLeft(AvlNode node)
        {
            
            if (node == null || node.Right == null)
            {
                return node;
            }

            AvlNode temp = node.Right;

            node.Left = temp.Left;

            temp.Left = node;

            UpdateHeight(node);
            UpdateHeight(temp);

            return temp;
        }
        public AvlNode Insert(AvlNode node, int value)
            {
                // STEP 1: Base case
                if (node == null)
                {
                    return new AvlNode(value);
                }

                // STEP 2: Normal BST insertion
                if (value < node.Value)
                {
                    node.Left = Insert(node.Left, value);
                }
                else if (value > node.Value)
                {
                    node.Right = Insert(node.Right, value);
                }
                else
                {
                    return node;
                }

                // STEP 3: Update height
                UpdateHeight(node);

                // STEP 4: Calculate balance factor
                int balance = GetHeight(node.Left) - GetHeight(node.Right);

                // STEP 5: Check rotations

                // LL Case
                if (balance > 1 && value < node.Left.Value)
                {
                    return RotateRight(node);
                }

                // RR Case
                if (balance < -1 && value > node.Right.Value)
                {
                    return RotateLeft(node);
                }

                // LR Case
                if (balance > 1 && value > node.Left.Value)
                {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }

                // RL Case
                if (balance < -1 && value < node.Right.Value)
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }

                return node;
            }

    public void PrintInOrder(AvlNode node)
        {
            if (node == null)
            {
                return;
            }

            PrintInOrder(node.Left);

            Console.WriteLine(node.Value);

            PrintInOrder(node.Right);
        }
    }
    public bool IsBalanced(AvlNode node)
        {
            if (node == null)
            {
                return true;
            }

            int balance = GetHeight(node.Left) - GetHeight(node.Right);

            if (balance < -1 || balance > 1)
            {
                return false;
            }

            return IsBalanced(node.Left) && IsBalanced(node.Right);
        }
    }
