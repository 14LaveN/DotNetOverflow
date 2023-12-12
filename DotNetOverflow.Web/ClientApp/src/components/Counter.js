import React from "react";

const URL = `https://localhost:7016/api/v1/article`;

const Posts = () => {
    const [allPosts, setPosts] = React.useState([]);

    const allPost = {
        Description: "fdgdfgdfdfg"
    };
    const getPosts = async () => {
        const options = {
            method: 'GET',
            headers: new Headers(),
            body: JSON.stringify(allPost)
        }
        const getArticlesURL = URL + '/get-all-articles';
        const result = await fetch(getArticlesURL, options);
        if (result.ok){
            const posts = await result.json();
            setPosts(posts);
            return posts;
        }
        return [];
    }

    const addPost = async () => {

        const Description = document.querySelector('#Description').value;
        const Title = document.querySelector('#Title').value;
        const Author = document.querySelector('#Author').value;
        const ContentType = document.querySelector('#ContentType').value;

        const addArticleURL = URL + '/create-article';
        const newPost = {
            Author: Author,
            Title: Title,
            Description: Description,
            ContentType: 1,
            AccountId: 1
        };

        const headers = new Headers();
        headers.set('Content-Type', 'application/json');
        headers.set('Authorization', 'Bearer     eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwibmFtZSI6InN0cmZkZ2RmZ2ZpbmciLCJlbWFpbCI6InN0cmluZ0BkZm1haWwucnUiLCJnaXZlbl9uYW1lIjoic3RyaW5mZ2hmZ2ciLCJmYW1pbHlfbmFtZSI6InN0ZmdocmluZyIsIm1pZGRsZV9uYW1lIjoic3RmZ2hpbmciLCJiaXJ0aGRhdGUiOiIwOS4xMi4yMDIzIiwic2NvcGUiOiJzdHJmZGdkZmdmaW5nIiwiZXhwIjoxNzAyMzM2NzU1LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTI0LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMjQvIn0.CnD2H1NiYawaOzCBPJn9N0XEN8rY8_gjeDMiw1BojAA')

        const options = {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(newPost)
        };

        const result = await fetch(addArticleURL, options);
        if (result.ok){
            const post = await result.json();
            allPosts.push(post);
            setPosts(allPosts.slice());
        }
    }

    React.useEffect(() => {
        getPosts();
    }, [])

    return (
        <div>
            <div>
                <p>Создание постов</p>
                <div style={{margin: '10px'}}>
                    <input id="Author" type="text" />
                </div>
                <div style={{margin: '10px'}}>
                    <textarea id="Title"/>
                </div>
                <select id="ContentType">
                    <option value="1">Easy</option>
                    <option value="2">Medium</option>
                    <option value="3">Hard</option>
                </select>
                <div style={{margin: '10px'}}>
                    <textarea  id="Description"/>
                </div>
                <button onClick={() => addPost()}>Add post</button>
            </div>
        </div>
    )
}

export default Posts;

const PostItem = ({post}) => {
    return (
        <div style={{backgroundColor: 'whitesmoke', margin: '10px', borderRadius: '10px', padding: '10px'}}>
            <h2>{post.Title}</h2>
            <p>{post.Description}</p>
            <p>{post.Author}</p>
            <p>{post.Priority}</p>
            <p>{post.CreationDate}</p>
        </div>
    )
}
