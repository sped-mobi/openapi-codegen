

## Terms

* **Dependent entity:** This is the entity that contains the foreign key property(s). Sometimes referred to as the 'child' of the relationship.
* **Principal entity:** This is the entity that contains the primary/alternate key property(s). Sometimes referred to as the 'parent' of the relationship.
* **Foreign Key:** The property(s) in the dependent entity that is used to store the values of the principal key property that the entity is related to.
* **Principal Key:** The property(s) that uniquely identifies the principal entity. This may be the primary key or an alternate key.
* **Navigation Property:** A property defined on the principal and/or dependent entity that contains a reference(s) to the related entity(s).
  * **Collection navigation property:** A navigation property that contains references to many related entities.
  * **Reference navigation property:** A navigation property that holds a reference to a single related entity.
  * **Inverse navigation property:** When discussing a particular navigation property, this term refers to the navigation property on the other end of the relationship.

The following code listing shows a one-to-many relationship between Blog and Post
* ```Post``` is the dependent entity
* ```Blog``` is the principal entity
* ```Post.BlogId``` is the principal key (in this case it is a primary key rather than an alternate key)
* ```Post.BlogId``` is the foreign key
* ```Post.Blog``` is a reference navigation property
* ```Blog.Posts``` is a collection navigation property
* ```Post.Blog``` is the inverse navigation property of ```Blog.Posts``` and vice versa.




``` csharp
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
```

