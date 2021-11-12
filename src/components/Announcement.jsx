import React from 'react';
import styled from 'styled-components';

const Container = styled.div`
    height: 30px;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: teal;
    color: white;
    font-weight: 300;
`

export default function Announcement() {
    return (
        <Container>Announcement</Container>
    )
}
