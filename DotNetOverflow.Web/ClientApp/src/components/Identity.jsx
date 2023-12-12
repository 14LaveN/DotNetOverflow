import React from "react";

const URL = `https://localhost:7004/api/v1/identity`;

const Posts = () => {
    const [allPosts, setPosts] = React.useState([]);

    const register = async () => {

        const Email = document.querySelector('#Email').value;
        const UserName = document.querySelector('#UserName').value;
        const RetypePassword = document.querySelector('#RetypePassword').value;
        const FirstName = document.querySelector('#FirstName').value;
        const LastName = document.querySelector('#LastName').value;
        const Patronymic = document.querySelector('#Patronymic').value;
        
        const addArticleURL = URL + '/register-user';
        const newPost = {
            Email: Email,
            UserName: UserName,
            RetypePassword: RetypePassword,
            FirstName: FirstName,
            LastName: LastName,
            Patronymic: Patronymic,
            BirthDate: "2023-12-11T04:52:43.798Z"
        };

        const headers = new Headers();
        headers.set('Content-Type', 'application/json');

        const options = {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(newPost)
        };

        const result = await fetch(addArticleURL, options);
        if (result.ok){
            const user = await result.json();
            headers.append('Authorization', 'Bearer' + user.value)
            allPosts.push(user);
            setPosts(allPosts.slice());
        }
    }
    
    return (
        <div>
            <div>
                <p>Создание постов</p>
                <div style={{margin: '10px'}}>
                    <input id="RetypePassword" type="text" />
                </div>
                <div style={{margin: '10px'}}>
                    <input id="Patronymic" type="text" />
                </div>
                <div style={{margin: '10px'}}>
                    <textarea id="UserName"/>
                </div>
                <input id="FirstName" type="text"/>
                <input id="LastName" type="text"/>
                <div style={{margin: '10px'}}>
                    <textarea  id="Email"/>
                </div>
                <button onClick={() => register()}>Register</button>
            </div>
        </div>
    )
}

export default Posts;

const PostItem = ({post}) => {
    return (
        <div style={{backgroundColor: 'whitesmoke', margin: '10px', borderRadius: '10px', padding: '10px'}}>
            <h2>{post.UserName}</h2>
            <p>{post.Email}</p>
            <p>{post.RetypePassword}</p>
            <p>{post.Priority}</p>
            <p>{post.CreationDate}</p>
        </div>
    )
}