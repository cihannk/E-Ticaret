import React from "react";
import styled from "styled-components";
import { useState } from "react";
import ArrowLeft from "@material-ui/icons/ChevronLeft";
import ArrowRight from "@material-ui/icons/ChevronRight";
import SliderItem from "./SliderItem";
import { mainPageProducts } from "../fakeData";

const Container = styled.div`
  width: 100%;
  height: 60vh;
  position: relative;
  display: flex;
  overflow: hidden;
  background-color: #f5fafd;
`;
const Arrow = styled.div`
    position: absolute;
    display: flex;
    align-items: center;
    z-index: 2;
    justify-content: center;
    border: 0.5px solid lightgray;
    border-radius: 50%;
    width: 50px;
    height: 50px;
    opacity: 0.5;
    top: 0;
    bottom: 0;
    margin: auto;
    left: ${(props) => props.direction === "left" && "10px"};
    right: ${(props) => props.direction === "right" && "10px"};
    &:hover {
      transition: all 0.5s ease;
      background-color: lightgray;
    }
`
const SliderWrapper = styled.div`
  display: flex;
  transform: translateX(${(props) => props.slideIndex * -100}vw);
  transition: all 1.5s ease;
`;


export default function Slider() {
    const [sliderXIndex, setSliderXIndex] = useState(0);
    const [lastSlideIndex, setLastSlideIndex] = useState(2);
    const handleSlide = (slideTo) =>{
        console.log(sliderXIndex);
        if (slideTo === "left"){
            sliderXIndex !== 0 && setSliderXIndex(prev => prev - 1);
        }
        else{
            sliderXIndex !== lastSlideIndex && setSliderXIndex(prev => prev + 1);
        }
    }
    console.log(sliderXIndex);
  return (
    <Container>
        <Arrow direction="left" onClick={()=> handleSlide("left")}>
            <ArrowLeft/>
        </Arrow>
      <SliderWrapper slideIndex={sliderXIndex}>
        {mainPageProducts.map(product =><SliderItem title={product.title} img={product.img} desc = {product.desc} key={product.id} id={product.id}/>)}
      </SliderWrapper>
      <Arrow direction="right" onClick={()=> handleSlide("right")}>
          <ArrowRight/>
      </Arrow>
    </Container>
  );
}
