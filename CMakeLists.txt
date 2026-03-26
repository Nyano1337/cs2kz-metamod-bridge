cmake_minimum_required(VERSION 3.22)

project(cs2kz-bridge)

include("shared.cmake")

set(CS2KZBRIDGE_SRC "${CMAKE_CURRENT_LIST_DIR}/src")
set(CS2KZBRIDGE_VENDOR "${CMAKE_CURRENT_LIST_DIR}/vendor")

include_directories(
	${CMAKE_CURRENT_LIST_DIR}/cs2kz-metamod/
	${CMAKE_CURRENT_LIST_DIR}/cs2kz-metamod/src/
	${CS2KZBRIDGE_SRC}
	${CS2KZBRIDGE_SRC}/protobuf
	${CS2KZBRIDGE_VENDOR}/libmodule/include
)

file(GLOB CS2KZBRIDGE_LIBS "${CS2KZBRIDGE_VENDOR}/libmodule/lib/*${CMAKE_STATIC_LIBRARY_SUFFIX}")

file(GLOB_RECURSE CS2KZBRIDGE_SOURCES "${CS2KZBRIDGE_SRC}/*.cpp" "${CS2KZBRIDGE_SRC}/*.h")

add_definitions(-DHAVE_MEMOVERRIDE_NEW_DELETE -DNOMINMAX)

add_library(${PROJECT_NAME} SHARED ${SDK_CONVAR} ${CS2KZBRIDGE_SOURCES})

target_link_libraries(${PROJECT_NAME} PRIVATE ${CS2KZBRIDGE_LIBS})

target_precompile_headers(${PROJECT_NAME} PRIVATE "${CS2KZBRIDGE_SRC}/pch.h")

set_target_properties(${PROJECT_NAME} PROPERTIES PREFIX "")

set(OUT_DIR "" CACHE PATH "cs2kz-bridge binary directory for post-build file copy")

if (NOT ${OUT_DIR} STREQUAL "")
	add_custom_command(
		TARGET ${PROJECT_NAME} POST_BUILD
		COMMAND ${CMAKE_COMMAND} -E copy
			$<TARGET_FILE:${PROJECT_NAME}>
			${OUT_DIR}
	)
endif()

