import React from 'react';
import styled from 'styled-components';
import Send from "@material-ui/icons/Send";

const Container = styled.div`
    height: 40vh;
    background-color: #f5fafd;
    align-items: center;
    justify-content: center;
    display: flex;
    flex-direction: column;
`
const NewsletterTitle = styled.span`
    font-size: 64px;
    font-weight: 600;
`
const NewsletterDesc = styled.span`
    font-size: 24px;
`
const NewsletterSubmit = styled.div`
    width: 50%;
    display: flex;
    margin-top: 24px;
`
const NewsletterInput = styled.input`
    flex: 5;
    padding: 10px;
    background-color: transparent;
    border: 1px solid lightgray;
    font-family: inherit;
`
const NewsletterButton= styled.button`
    flex: 1;
    background-color: transparent;
    border: 1px solid lightgray;
    &:hover{
        background-color: #f7f3f3;
        transition: all 0.5s ease;
    }
`

export default function Newsletter() {
    return (
        <Container>
                <NewsletterTitle>Newsletter</NewsletterTitle>
                <NewsletterDesc>Get timely updates from favourite products.</NewsletterDesc>
                <NewsletterSubmit>
                    <NewsletterInput/>
                    <NewsletterButton>
                        <Send style={{color: "gray"}}/>
                    </NewsletterButton>
                </NewsletterSubmit>
        </Container>
    )
}
