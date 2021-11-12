import React from 'react';
import Announcement from '../components/Announcement';
import Navbar from '../components/Navbar';
import Newsletter from '../components/Newsletter';
import ProductCategories from '../components/ProductCategories';
import Slider from '../components/Slider';


export default function Home() {
    return (
        <div>
            <Navbar/>
            <Announcement/>
            <Slider/>
            <ProductCategories/>
            <Newsletter/>
        </div>
        
    )
}
