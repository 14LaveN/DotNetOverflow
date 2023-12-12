import { useEffect, useState } from "react";

const QuestionURL = `https://localhost:7120/api/v1/question`;
const ImageURL = 'https://localhost:7048/api/v1/image/create-image'

const Posts = () => {
    const addImage = async () => {

        const Image = document.querySelector('#Image').value;
        const DefImage = Image.readAsArrayBuffer().slice(0);
        const newPost = {
            Image: DefImage,
            QuestionId: "b7fe3520-3019-4d3b-b0e3-ce26dedf351f"
        };

        const headers = new Headers();
        headers.set('Content-Type', 'application/json');
        headers.set('Authorization', 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3IiwibmFtZSI6ImRzZmRzZnNkZGZnZ2dkZmdkZmdmZiIsImVtYWlsIjoiTUFzZGprc2RqQG1haWwucnUiLCJnaXZlbl9uYW1lIjoiZHNmc2Rmc2QiLCJmYW1pbHlfbmFtZSI6ImRzZnNkZiIsIm1pZGRsZV9uYW1lIjoiZHNmZmRzZiIsImJpcnRoZGF0ZSI6IjExLjEyLjIwMjMiLCJleHAiOjE3MDI1MTA3NjgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMjQvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzEyNC8ifQ.oDZpEHC-ac8asy44LhQ-7wkH4hdKW4CaUKXy0qPbSvk')

        const options = {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(newPost)
        };

        const result = await fetch(ImageURL, options);
        if (result.ok){
            const post = await result.json();
            //allPosts.push(post);
            //setPosts(allPosts.slice());
        }
    }
    const registers = async () => {

        const Body = document.querySelector('#Body').value;
        const Title = document.querySelector('#Title').value;
        const Image = document.querySelector('#Image').value;
        const QuestionType = document.querySelector('#QuestionType').value;
        const Tag = document.querySelector('#Tag').value;
        
        const addArticleQuestionURL = QuestionURL + '/create-question';
        const newPost = {
            Title: Title,
            //Image: Image,
            Body: Body,
            Tag: Tag,
            AuthorId: 1,
            QuestionType: 1
        };

        const headers = new Headers();
        headers.set('Content-Type', 'application/json');
        headers.set('Authorization', 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3IiwibmFtZSI6ImRzZmRzZnNkZGZnZ2dkZmdkZmdmZiIsImVtYWlsIjoiTUFzZGprc2RqQG1haWwucnUiLCJnaXZlbl9uYW1lIjoiZHNmc2Rmc2QiLCJmYW1pbHlfbmFtZSI6ImRzZnNkZiIsIm1pZGRsZV9uYW1lIjoiZHNmZmRzZiIsImJpcnRoZGF0ZSI6IjExLjEyLjIwMjMiLCJleHAiOjE3MDI1MTA3NjgsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMjQvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzEyNC8ifQ.oDZpEHC-ac8asy44LhQ-7wkH4hdKW4CaUKXy0qPbSvk')

        const options = {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(newPost)
        };

        const result = await fetch(addArticleQuestionURL, options);
        if (result.ok){
            const post = await result.json();
           //allPosts.push(post);
           //setPosts(allPosts.slice());
        }
    }
    
    return (
        <div>
            <div>
                <p>Создание постов</p>
                <input id="Tag" type="text" />
                <div style={{margin: '10px'}}>
                    <textarea id="Title"/>
                </div>
                <input type="file" id="Image" />
                <select id="QuestionType">
                    <option value="1">Easy</option>
                    <option value="2">Medium</option>
                    <option value="3">Hard</option>
                </select>
                <div style={{margin: '10px'}}>
                    <textarea  id="Body"/>
                </div>
                <button onClick={() => registers()}>Register</button>
                <button  onClick={() => addImage()}>Add Image</button>
            </div>
        </div>
    )
}

export default Posts;

const PostItem = ({post}) => {
    return (
        <div style={{backgroundColor: 'whitesmoke', margin: '10px', borderRadius: '10px', padding: '10px'}}>
            <h2>{post.Title}</h2>
            <p>{post.Body}</p>
            <p>{post.AuthorId}</p>
            <p>{post.Priority}</p>
            <p>{post.CreationDate}</p>
        </div>
    )
}